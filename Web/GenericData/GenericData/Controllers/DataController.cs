using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System;
using System.Collections;
using System.Linq.Expressions;

namespace GenericData.Controllers
{
    [RoutePrefix("api/data")]
    public class DataController : ApiController
    {
        [HttpGet]
        [Route("{set}")]
        public IQueryable<JToken> Get(string set)
        {
            return new CustomIQueryable<JToken>(set);
        }

        private IEnumerable<Item> GetData()
        {
            for (int i = 0; i < 3; i++)
            {
                yield return //JToken.FromObject(
                    new Item
                    {
                        Name = $"Item {i + 1}",
                        Value = i
                    }
                    ;
                    //);
            }
        }
    }

    public class CustomIQueryable<T> : IQueryable<T>
    {
        public string Set { get; private set; }
        public CustomIQueryable(string set)
        {
            Set = set;
            Provider = new CustomIQueryProvider(set);
            Expression = Expression.Constant(this);
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public IQueryProvider Provider { get; private set; }
        public Expression Expression { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<T>>(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (Provider.Execute<IEnumerable>(Expression)).GetEnumerator();
        }
    }

    public class CustomIQueryProvider : IQueryProvider
    {
        public string Set { get; private set; }

        public CustomIQueryProvider(string set)
        {
            Set = set;
        }
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
