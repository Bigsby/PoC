using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.DependencyInjection;
using Remotion.Linq.Clauses;
using Remotion.Linq.Parsing.ExpressionVisitors.TreeEvaluation;

public class LocalOptionsExtension : IDbContextOptionsExtension
{
    public string LogFragment => string.Empty;

    internal string RootFolder;

    public bool ApplyServices(IServiceCollection services)
    {
        services.AddLocalDatabase();

        return true;
    }

    public long GetServiceProviderHashCode() => RootFolder?.GetHashCode() ?? 0L;

    public void Validate(IDbContextOptions options)
    {
    }
}

public class LocalValueGeneratorSelector : ValueGeneratorSelector
{
    public LocalValueGeneratorSelector(ValueGeneratorSelectorDependencies dependencies) : base(dependencies)
    {
    }

    public override ValueGenerator Create(Microsoft.EntityFrameworkCore.Metadata.IProperty property, Microsoft.EntityFrameworkCore.Metadata.IEntityType entityType)
    {
        return base.Create(property, entityType);
    }
}

public interface ILocalDbDatabase : IDatabase
{ }

public class LocalDbDatabase : Database, ILocalDbDatabase
{
    protected LocalDbDatabase(DatabaseDependencies dependencies) : 
        base(dependencies)
    {
    }

    public override int SaveChanges(IReadOnlyList<IUpdateEntry> entries)
    {
        return entries.Count;
    }

    public override Task<int> SaveChangesAsync(IReadOnlyList<IUpdateEntry> entries, CancellationToken cancellationToken = default(CancellationToken))
    {
        return Task.FromResult(SaveChanges(entries));
    }
}

public class LocalDbTransaction : IDbContextTransaction
{
    public Guid TransactionId { get; } = Guid.NewGuid();

    public void Commit()
    {
    }

    public void Dispose()
    {
    }

    public void Rollback()
    {
    }
}

public class LocaDbTransactionManager : IDbContextTransactionManager
{
    public IDbContextTransaction CurrentTransaction => null;

    private static readonly LocalDbTransaction _transaction = new LocalDbTransaction();

    public IDbContextTransaction BeginTransaction()
    {
        return _transaction;
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        return Task.FromResult<IDbContextTransaction>(_transaction);
    }

    public void CommitTransaction()
    {
    }

    public void ResetState()
    {
    }

    public void RollbackTransaction()
    {
    }
}

public class LocalDbDatabaseCreator : IDatabaseCreator
{
    private readonly ILocalDbDatabase _localDb;

    public LocalDbDatabaseCreator(ILocalDbDatabase localDb)
    {
        _localDb = localDb;
    }

    public bool EnsureCreated()
    {
        return true;
    }

    public Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        return Task.FromResult(EnsureCreated());
    }

    public bool EnsureDeleted()
    {
        return true;
    }

    public Task<bool> EnsureDeletedAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        return Task.FromResult(EnsureDeleted());
    }
}

public class LocalDbQueryContext : QueryContext
{
    public LocalDbQueryContext(QueryContextDependencies dependencies, Func<IQueryBuffer> queryBufferFactory) 
        : base(dependencies, queryBufferFactory)
    {
    }
}

public class LocalDbQueryContextFactory : QueryContextFactory
{
    public LocalDbQueryContextFactory(QueryContextDependencies dependencies)
        : base(dependencies)
    {
    }

    public override QueryContext Create() => new LocalDbQueryContext(Dependencies, CreateQueryBuffer);
}

public class LocalDbEntityQueryModelVisitor : EntityQueryModelVisitor
{
    public LocalDbEntityQueryModelVisitor(EntityQueryModelVisitorDependencies dependencies, QueryCompilationContext queryCompilationContext) 
        : base(dependencies, queryCompilationContext)
    {
    }


}

public class LocalDbEntityQueryModelVisitorFactory : EntityQueryModelVisitorFactory
{
    public LocalDbEntityQueryModelVisitorFactory(EntityQueryModelVisitorDependencies dependencies) : base(dependencies)
    {
    }

    public override EntityQueryModelVisitor Create(QueryCompilationContext queryCompilationContext, EntityQueryModelVisitor parentEntityQueryModelVisitor)
    => new LocalDbEntityQueryModelVisitor(Dependencies, queryCompilationContext);
}

public class LocalDbEntityQueryableExpressionVisitor : EntityQueryableExpressionVisitor
{
    public LocalDbEntityQueryableExpressionVisitor(EntityQueryModelVisitor entityQueryModelVisitor) : 
        base(entityQueryModelVisitor)
    {
    }

    protected override Expression VisitEntityQueryable(Type elementType)
    {
        return null;
        //var entityType = QueryModelVisitor.QueryCompilationContext.FindEntityType(_querySource)
        //                     ?? _model.FindEntityType(elementType);

        //if (QueryModelVisitor.QueryCompilationContext
        //    .QuerySourceRequiresMaterialization(_querySource))
        //{
        //    var materializer = _materializerFactory.CreateMaterializer(entityType);

        //    return Expression.Call(
        //        InMemoryQueryModelVisitor.EntityQueryMethodInfo.MakeGenericMethod(elementType),
        //        EntityQueryModelVisitor.QueryContextParameter,
        //        Expression.Constant(entityType),
        //        Expression.Constant(entityType.FindPrimaryKey()),
        //        materializer,
        //        Expression.Constant(QueryModelVisitor.QueryCompilationContext.IsTrackingQuery));
        //}

        //return Expression.Call(
        //    InMemoryQueryModelVisitor.ProjectionQueryMethodInfo,
        //    EntityQueryModelVisitor.QueryContextParameter,
        //    Expression.Constant(entityType));
    }
}

public class LocalDbEntityQueryableExpressionVisitorFactory : IEntityQueryableExpressionVisitorFactory
{
    public ExpressionVisitor Create(EntityQueryModelVisitor entityQueryModelVisitor, IQuerySource querySource)
        => new LocalDbEntityQueryableExpressionVisitor(entityQueryModelVisitor);
}

public static class LocalServiceCollectionExtension
{
    public static IServiceCollection AddLocalDatabase(this IServiceCollection serviceCollection)
    {
        var builder = new EntityFrameworkServicesBuilder(serviceCollection)
            .TryAdd<IDatabaseProvider, DatabaseProvider<LocalOptionsExtension>>()
            .TryAdd<IValueGeneratorSelector, LocalValueGeneratorSelector>()
            .TryAdd<IDatabase>(p => p.GetService<ILocalDbDatabase>())
            .TryAdd<IDbContextTransactionManager, LocaDbTransactionManager>()
            .TryAdd<IDatabaseCreator, LocalDbDatabaseCreator>()
            .TryAdd<IQueryContextFactory, LocalDbQueryContextFactory>()
            .TryAdd<IEntityQueryModelVisitorFactory, LocalDbEntityQueryModelVisitorFactory>()
            .TryAdd<IEntityQueryableExpressionVisitorFactory, LocalDbEntityQueryableExpressionVisitorFactory>()
            .TryAdd<IEvaluatableExpressionFilter, EvaluatableExpressionFilter>()
            //.TryAdd<ISingletonOptions, IInMemorySingletonOptions>(p => p.GetService<IInMemorySingletonOptions>())
            .TryAddProviderSpecificServices(
                b => b
                    //.TryAddSingleton<IInMemorySingletonOptions, InMemorySingletonOptions>()
                    //.TryAddSingleton<IInMemoryStoreCache, InMemoryStoreCache>()
                    //.TryAddSingleton<IInMemoryTableFactory, InMemoryTableFactory>()
                    .TryAddScoped<ILocalDbDatabase, LocalDbDatabase>()
                    .TryAddScoped<IMaterializerFactory, MaterializerFactory>());

        builder.TryAddCoreServices();

        return serviceCollection;
    }
}

public static class LocalDbContextOptionsExtensions
{
    public static DbContextOptionsBuilder UseLocalDatabase(
            this DbContextOptionsBuilder optionsBuilder,
            string rootFolder)
    {

        var extension = optionsBuilder.Options.FindExtension<LocalOptionsExtension>()
                           ?? new LocalOptionsExtension();


        if (rootFolder != null)
        {
            extension.RootFolder = rootFolder;
        }

            //ConfigureWarnings(optionsBuilder);

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

        //inMemoryOptionsAction?.Invoke(new InMemoryDbContextOptionsBuilder(optionsBuilder));

        return optionsBuilder;
    }
}