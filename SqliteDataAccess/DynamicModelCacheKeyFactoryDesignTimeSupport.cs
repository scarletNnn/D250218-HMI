using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SqliteDataAccess;

class DynamicModelCacheKeyFactoryDesignTimeSupport : IModelCacheKeyFactory
{
    public object Create(DbContext context, bool designTime) =>
        context is MyDbContext myDbContext
            ? (context.GetType(), myDbContext.CreateDateTime, designTime)
            : (object)context.GetType();

    public object Create(DbContext context) => Create(context, false);
}
