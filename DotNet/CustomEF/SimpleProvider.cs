using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using System.Linq.Expressions;
using Remotion.Linq.Clauses;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomEF
{
    public class SimpleEntityQueryModelVisitor : EntityQueryModelVisitor
    {
        private static IEnumerable<TEntity> EntityQuery<TEntity>(
           QueryContext queryContext,
           IEntityType entityType,
           IKey key,
           Func<IEntityType, ValueBuffer, object> materializer,
           bool queryStateManager)
           where TEntity : class
           => ((InMemoryQueryContext)queryContext).Store
               .GetTables(entityType)
               .SelectMany(
                   t =>
                       t.Rows.Select(
                           vs =>
                           {
                               var valueBuffer = new ValueBuffer(vs);

                               return (TEntity)queryContext
                                    .QueryBuffer
                                    .GetEntity(
                                        key,
                                        new EntityLoadInfo(
                                            valueBuffer,
                                            vr => materializer(t.EntityType, vr)),
                                        queryStateManager,
                                        throwOnNullKey: false);
                           }));
    }

    public class SimpleEntityQueryableExpressionVisitor : EntityQueryableExpressionVisitor
    {
        private readonly IModel _model;
        private readonly IQuerySource _querySource;

        public SimpleEntityQueryableExpressionVisitor(
            EntityQueryModelVisitor entityQueryModelVisitor,
            IModel model,
            IQuerySource querySource) :
            base(entityQueryModelVisitor)
        {
            _model = model;
            _querySource = querySource;
        }

        protected override Expression VisitEntityQueryable(Type elementType)
        {
            
            var entityType = QueryModelVisitor.QueryCompilationContext.FindEntityType(_querySource)
                                 ?? _model.FindEntityType(elementType);


            if (QueryModelVisitor.QueryCompilationContext
                .QuerySourceRequiresMaterialization(_querySource))
            {
                var materializer = _materializerFactory.CreateMaterializer(entityType);

                return Expression.Call(
                    InMemoryQueryModelVisitor.EntityQueryMethodInfo.MakeGenericMethod(elementType),
                    EntityQueryModelVisitor.QueryContextParameter,
                    Expression.Constant(entityType),
                    Expression.Constant(entityType.FindPrimaryKey()),
                    materializer,
                    Expression.Constant(QueryModelVisitor.QueryCompilationContext.IsTrackingQuery));
            }

            return Expression.Call(
                InMemoryQueryModelVisitor.ProjectionQueryMethodInfo,
                EntityQueryModelVisitor.QueryContextParameter,
                Expression.Constant(entityType));
        }
    }

    public class SimpleEntityQueryableExpressionVisitorFactory : IEntityQueryableExpressionVisitorFactory
    {
        private readonly IModel _model;

        public SimpleEntityQueryableExpressionVisitorFactory(IModel model)
        {
            _model = model;
        }

        public ExpressionVisitor Create(EntityQueryModelVisitor entityQueryModelVisitor, IQuerySource querySource)
            => new SimpleEntityQueryableExpressionVisitor(entityQueryModelVisitor, _model, querySource);
    }

    public class SimpleEntityQueryModelVisitor : EntityQueryModelVisitor
    {
        public SimpleEntityQueryModelVisitor(EntityQueryModelVisitorDependencies dependencies, QueryCompilationContext queryCompilationContext)
            : base(dependencies, queryCompilationContext)
        {
        }

        public static readonly MethodInfo ProjectionQueryMethodInfo
         = typeof(InMemoryQueryModelVisitor).GetTypeInfo()
             .GetDeclaredMethod(nameof(ProjectionQuery));

        private static IEnumerable<ValueBuffer> ProjectionQuery(
            QueryContext queryContext,
            IEntityType entityType)
            => new ValueBuffer[0];
            //((SimpleQueryContext)queryContext).Store
            //    .GetTables(entityType)
            //    .SelectMany(t => t.Rows.Select(vs => new ValueBuffer(vs)));
    }

    public class SimpleEntityQueryModelVisitorFactory : EntityQueryModelVisitorFactory
    {
        public SimpleEntityQueryModelVisitorFactory(EntityQueryModelVisitorDependencies dependencies) : base(dependencies)
        {
        }

        public override EntityQueryModelVisitor Create(QueryCompilationContext queryCompilationContext, EntityQueryModelVisitor parentEntityQueryModelVisitor)
        => new SimpleEntityQueryModelVisitor(Dependencies, queryCompilationContext);
    }

    public class SimpleDatabase : Database
    {
        public SimpleDatabase(DatabaseDependencies dependencies) 
            : base(dependencies)
        {
        }

        public override int SaveChanges(IReadOnlyList<IUpdateEntry> entries)
        {
            return 0;
        }

        public override Task<int> SaveChangesAsync(IReadOnlyList<IUpdateEntry> entries, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(SaveChanges(entries));
        }
    }

    public class SimpleDbContextOptionsExtension : IDbContextOptionsExtension
    {
        public string LogFragment => string.Empty;

        public bool ApplyServices(IServiceCollection services)
        {
            services.AddSimple();
            return true;
        }

        public long GetServiceProviderHashCode() => GetHashCode();

        public void Validate(IDbContextOptions options) { }
    }

    public class SimpleQueryContext : QueryContext
    {
        public SimpleQueryContext(QueryContextDependencies dependencies, Func<IQueryBuffer> queryBufferFactory) 
            : base(dependencies, queryBufferFactory)
        { }
    }

    public class SimpleQueryContextFactory : QueryContextFactory
    {
        public SimpleQueryContextFactory(QueryContextDependencies dependencies) 
            : base(dependencies)
        { }

        public override QueryContext Create() => new SimpleQueryContext(Dependencies, CreateQueryBuffer);
    }

    public static class SimpleServiceCollectionExtension
    {
        public static IServiceCollection AddSimple(this IServiceCollection serviceCollection)
        {
            var builder = new EntityFrameworkServicesBuilder(serviceCollection);
            builder.TryAdd<IDatabaseProvider, DatabaseProvider<SimpleDbContextOptionsExtension>>();
            builder.TryAdd<IQueryContextFactory, SimpleQueryContextFactory>();
            builder.TryAdd<IDatabase, SimpleDatabase>();
            builder.TryAdd<IEntityQueryModelVisitorFactory, SimpleEntityQueryModelVisitorFactory>();
            builder.TryAdd<IEntityQueryableExpressionVisitorFactory, SimpleEntityQueryableExpressionVisitorFactory>();
            builder.TryAddCoreServices();
            return serviceCollection;
        }

        public static DbContextOptionsBuilder UseSimple(
            this DbContextOptionsBuilder optionsBuilder)
        {

            var extension = optionsBuilder.Options.FindExtension<SimpleDbContextOptionsExtension>()
                               ?? new SimpleDbContextOptionsExtension();

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            return optionsBuilder;
        }
    }

    internal static class SimpleModelBuilder
    {
    }

    public static class ModelBuilderExtensions
    {
        private const string DefaultDatabaseName = "data";
        private const string DatabaseAnnotation = "Simple_Database";
        private const string CollectionAnnotation = "Simple_Collection";

        public static EntityTypeBuilder<T> ToCollection<T>(this EntityTypeBuilder<T> etBuilder, string collection)
            where T : class
        {
            etBuilder.Metadata.AddAnnotation(CollectionAnnotation, collection);
            return etBuilder;
        }

        public static EntityTypeBuilder<T> ToCollection<T>(this EntityTypeBuilder<T> etBuilder, string database, string collection)
            where T : class
        {
            etBuilder.Metadata.AddAnnotation(DatabaseAnnotation, database);
            etBuilder.Metadata.AddAnnotation(CollectionAnnotation, collection);
            return etBuilder;
        }
    }
}
