﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SqlDbLite
{
    public partial interface IDbLiteQueryable<T>
    {
        SqlDbLiteProvider Context { get; set; }
        ISqlBuilder SqlBuilder { get; set; }
        QueryBuilder QueryBuilder { get; set; }
        IDbLiteQueryable<T> Clone();
        IDbLiteQueryable<T> AS<T2>(string tableName);
        IDbLiteQueryable<T> AS(string tableName);
        IDbLiteQueryable<T> With(string withString);
        IDbLiteQueryable<T> Filter(string FilterName, bool isDisabledGobalFilter = false);
        IDbLiteQueryable<T> Mapper(Action<T> mapperAction);
        IDbLiteQueryable<T> Mapper(Action<T, MapperCache<T>> mapperAction);
        IDbLiteQueryable<T> Mapper<TObject>(Expression<Func<T, TObject>> mapperObject, Expression<Func<T, object>> mainField, Expression<Func<T, object>> childField);
        IDbLiteQueryable<T> Mapper<TObject>(Expression<Func<T, List<TObject>>> mapperObject, Expression<Func<T, object>> mainField, Expression<Func<T, object>> childField);
        IDbLiteQueryable<T> Mapper<TObject>(Expression<Func<T, TObject>> mapperObject, Expression<Func<T, object>> mapperField);
        IDbLiteQueryable<T> Mapper<TObject>(Expression<Func<T, List<TObject>>> mapperObject, Expression<Func<T, object>> mapperField);
        IDbLiteQueryable<T> AddParameters(object parameters);
        IDbLiteQueryable<T> AddParameters(DbLiteParameter[] parameters);
        IDbLiteQueryable<T> AddParameters(List<DbLiteParameter> parameters);
        IDbLiteQueryable<T> AddJoinInfo(string tableName, string shortName, string joinWhere, JoinType type = JoinType.Left);

        /// <summary>
        /// if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClass"></param>
        /// <returns></returns>
        IDbLiteQueryable<T> WhereClass<ClassType>(ClassType whereClass,bool ignoreDefaultValue=false)where ClassType: class,new();
        /// <summary>
        ///  if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClassTypes"></param>
        /// <returns></returns>
        IDbLiteQueryable<T> WhereClass<ClassType>(List<ClassType> whereClassList,bool ignoreDefaultValue = false) where ClassType : class, new(); 

        IDbLiteQueryable<T> Where(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T> Where(string whereString, object parameters = null);
        IDbLiteQueryable<T> Where(List<IConditionalModel> conditionalModels);

        IDbLiteQueryable<T> Having(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T> Having(string whereString, object parameters = null);

        IDbLiteQueryable<T> WhereIF(bool isWhere, Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T> WhereIF(bool isWhere, string whereString, object parameters = null);

        T InSingle(object pkValue);
        Task<T> InSingleAsync(object pkValue);
        IDbLiteQueryable<T> In<TParamter>(params TParamter[] pkValues);
        IDbLiteQueryable<T> In<FieldType>(string InFieldName, params FieldType[] inValues);
        IDbLiteQueryable<T> In<FieldType>(Expression<Func<T, object>> expression, params FieldType[] inValues);
        IDbLiteQueryable<T> In<TParamter>(List<TParamter> pkValues);
        IDbLiteQueryable<T> In<FieldType>(string InFieldName, List<FieldType> inValues);
        IDbLiteQueryable<T> In<FieldType>(Expression<Func<T, object>> expression, List<FieldType> inValues);
        IDbLiteQueryable<T> In<FieldType>(Expression<Func<T, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);

        IDbLiteQueryable<T> OrderBy(string orderFileds);
        IDbLiteQueryable<T> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T> OrderByIF(bool isOrderBy, string orderFileds);
        IDbLiteQueryable<T> OrderByIF(bool isOrderBy, Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);


        IDbLiteQueryable<T> GroupBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T> GroupBy(string groupFileds);

        IDbLiteQueryable<T> PartitionBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T> PartitionBy(string groupFileds);

        IDbLiteQueryable<T> Skip(int index);
        IDbLiteQueryable<T> Take(int num);
        IDbLiteQueryable<T> Distinct();

        T Single();
        Task<T> SingleAsync();
        T Single(Expression<Func<T, bool>> expression);
        Task<T> SingleAsync(Expression<Func<T, bool>> expression);

        T First();
        Task<T> FirstAsync();
        T First(Expression<Func<T, bool>> expression);
        Task<T> FirstAsync(Expression<Func<T, bool>> expression);

        bool Any(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        bool Any();
        Task<bool> AnyAsync();

        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>();
        IDbLiteQueryable<TResult> Select<TResult>(string select);
        IDbLiteQueryable<T> Select(string select);
        IDbLiteQueryable<T> MergeTable();

        int Count();
        Task<int> CountAsync();
        int Count(Expression<Func<T, bool>> expression);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        TResult Max<TResult>(string maxField);
        Task<TResult> MaxAsync<TResult>(string maxField);
        TResult Max<TResult>(Expression<Func<T, TResult>> expression);
        Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> expression);
        TResult Min<TResult>(string minField);
        Task<TResult> MinAsync<TResult>(string minField);
        TResult Min<TResult>(Expression<Func<T, TResult>> expression);
        Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> expression);
        TResult Sum<TResult>(string sumField);
        Task<TResult> SumAsync<TResult>(string sumField);
        TResult Sum<TResult>(Expression<Func<T, TResult>> expression);
        Task<TResult> SumAsync<TResult>(Expression<Func<T, TResult>> expression);
        TResult Avg<TResult>(string avgField);
        Task<TResult> AvgAsync<TResult>(string avgField);
        TResult Avg<TResult>(Expression<Func<T, TResult>> expression);
        Task<TResult> AvgAsync<TResult>(Expression<Func<T, TResult>> expression);

        List<T> ToList();
        T[] ToArray();
        Task<List<T>> ToListAsync();

        string ToJson();
        Task<string> ToJsonAsync();
        string ToJsonPage(int pageIndex, int pageSize);
        Task<string> ToJsonPageAsync(int pageIndex, int pageSize);
        string ToJsonPage(int pageIndex, int pageSize, ref int totalNumber);
        Task<string> ToJsonPageAsync(int pageIndex, int pageSize, RefAsync<int> totalNumber);
        KeyValuePair<string, List<DbLiteParameter>> ToSql();


        DataTable ToDataTable();
        Task<DataTable> ToDataTableAsync();
        DataTable ToDataTablePage(int pageIndex, int pageSize);
        Task<DataTable> ToDataTablePageAsync(int pageIndex, int pageSize);
        DataTable ToDataTablePage(int pageIndex, int pageSize, ref int totalNumber);
        DataTable ToDataTablePage(int pageIndex, int pageSize, ref int totalNumber,ref int totalPage);
        Task<DataTable> ToDataTablePageAsync(int pageIndex, int pageSize, RefAsync<int> totalNumber);

        List<T> ToPageList(int pageIndex, int pageSize);
        Task<List<T>> ToPageListAsync(int pageIndex, int pageSize);
        List<T> ToPageList(int pageIndex, int pageSize, ref int totalNumber);
        List<T> ToPageList(int pageIndex, int pageSize, ref int totalNumber,ref int totalPage);
        Task<List<T>> ToPageListAsync(int pageIndex, int pageSize, RefAsync<int> totalNumber);
        IDbLiteQueryable<T> WithCache(int cacheDurationInSeconds = int.MaxValue);
        IDbLiteQueryable<T> WithCacheIF(bool isCache, int cacheDurationInSeconds = int.MaxValue);
        string ToClassString(string className);
        void Clear();
        void AddQueue();
        IDbLiteQueryable<T> IgnoreColumns(Expression<Func<T, object>> columns);
        IDbLiteQueryable<T> IgnoreColumns(params string[] columns);
    }
    public partial interface IDbLiteQueryable<T, T2> : IDbLiteQueryable<T>
    {
        #region Where
        new IDbLiteQueryable<T, T2> Where(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2> Where(Expression<Func<T, T2, bool>> expression);
        new IDbLiteQueryable<T, T2> Where(List<IConditionalModel> conditionalModels);

        new IDbLiteQueryable<T, T2> WhereIF(bool isWhere, Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2> WhereIF(bool isWhere, Expression<Func<T, T2, bool>> expression);

        new IDbLiteQueryable<T, T2> Where(string whereString, object whereObj = null);
        new IDbLiteQueryable<T, T2> WhereIF(bool isWhere, string whereString, object whereObj = null);
        /// <summary>
        /// if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClass"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T,T2> WhereClass<ClassType>(ClassType whereClass, bool ignoreDefaultValue = false) where ClassType : class, new();
        /// <summary>
        ///  if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClassTypes"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T,T2> WhereClass<ClassType>(List<ClassType> whereClassList, bool ignoreDefaultValue = false) where ClassType : class, new();
        #endregion

        #region Select
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, TResult>> expression);
        #endregion

        #region OrderBy
        new IDbLiteQueryable<T, T2> OrderBy(string orderFileds);
        new IDbLiteQueryable<T, T2> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2> OrderBy(Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        new IDbLiteQueryable<T, T2> OrderByIF(bool isOrderBy, string orderFileds);
        new IDbLiteQueryable<T, T2> OrderByIF(bool isOrderBy, Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2> OrderByIF(bool isOrderBy, Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        #endregion

        #region GroupBy
        new IDbLiteQueryable<T, T2> GroupBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T, T2> GroupBy(Expression<Func<T, T2, object>> expression);
        new IDbLiteQueryable<T, T2> Having(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2> Having(Expression<Func<T, T2, bool>> expression);
        new IDbLiteQueryable<T, T2> Having(string whereString, object whereObj = null);
        #endregion

        #region Aggr
        TResult Max<TResult>(Expression<Func<T, T2, TResult>> expression);
        TResult Min<TResult>(Expression<Func<T, T2, TResult>> expression);
        TResult Sum<TResult>(Expression<Func<T, T2, TResult>> expression);
        TResult Avg<TResult>(Expression<Func<T, T2, TResult>> expression);
        #endregion

        #region In
        new IDbLiteQueryable<T, T2> In<FieldType>(Expression<Func<T, object>> expression, params FieldType[] inValues);
        new IDbLiteQueryable<T, T2> In<FieldType>(Expression<Func<T, object>> expression, List<FieldType> inValues);
        new IDbLiteQueryable<T, T2> In<FieldType>(Expression<Func<T, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);

        IDbLiteQueryable<T, T2> In<FieldType>(Expression<Func<T,T2, object>> expression, params FieldType[] inValues);
        IDbLiteQueryable<T, T2> In<FieldType>(Expression<Func<T,T2, object>> expression, List<FieldType> inValues);
        IDbLiteQueryable<T, T2> In<FieldType>(Expression<Func<T,T2, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);
        #endregion

        #region Other
        new IDbLiteQueryable<T, T2> Clone();
        new IDbLiteQueryable<T, T2> AS<AsT>(string tableName);
        new IDbLiteQueryable<T, T2> AS(string tableName);
        new IDbLiteQueryable<T, T2> Filter(string FilterName, bool isDisabledGobalFilter = false);
        new IDbLiteQueryable<T, T2> AddParameters(object parameters);
        new IDbLiteQueryable<T, T2> AddParameters(DbLiteParameter[] parameters);
        new IDbLiteQueryable<T, T2> AddParameters(List<DbLiteParameter> parameters);
        new IDbLiteQueryable<T, T2> AddJoinInfo(string tableName, string shortName, string joinWhere, JoinType type = JoinType.Left);
        new IDbLiteQueryable<T, T2> With(string withString);
        new IDbLiteQueryable<T, T2> WithCache(int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2> WithCacheIF(bool isCache, int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T,T2> Distinct();
        bool Any(Expression<Func<T,T2, bool>> expression);
        #endregion
    }
    public partial interface IDbLiteQueryable<T, T2, T3> : IDbLiteQueryable<T>
    {
        #region Where
        new IDbLiteQueryable<T, T2, T3> Where(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3> Where(Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3> Where(Expression<Func<T, T2, T3, bool>> expression);
        new IDbLiteQueryable<T, T2, T3> Where(List<IConditionalModel> conditionalModels);

        new IDbLiteQueryable<T, T2, T3> WhereIF(bool isWhere, Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3> WhereIF(bool isWhere, Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3> WhereIF(bool isWhere, Expression<Func<T, T2, T3, bool>> expression);

        new IDbLiteQueryable<T, T2, T3> Where(string whereString, object parameters = null);
        new IDbLiteQueryable<T, T2, T3> WhereIF(bool isWhere, string whereString, object parameters = null);

        /// <summary>
        /// if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClass"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T, T2,T3> WhereClass<ClassType>(ClassType whereClass, bool ignoreDefaultValue = false) where ClassType : class, new();
        /// <summary>
        ///  if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClassTypes"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T, T2,T3> WhereClass<ClassType>(List<ClassType> whereClassList, bool ignoreDefaultValue = false) where ClassType : class, new();
        #endregion

        #region Select
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        #endregion

        #region OrderBy
        new IDbLiteQueryable<T, T2, T3> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3> OrderBy(Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3> OrderBy(Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        new IDbLiteQueryable<T, T2, T3> OrderByIF(bool isOrderBy, string orderFileds);
        new IDbLiteQueryable<T, T2, T3> OrderByIF(bool isOrderBy, Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3> OrderByIF(bool isOrderBy, Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        #endregion

        #region GroupBy
        new IDbLiteQueryable<T, T2, T3> GroupBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T, T2, T3> GroupBy(Expression<Func<T, T2, object>> expression);
        IDbLiteQueryable<T, T2, T3> GroupBy(Expression<Func<T, T2, T3, object>> expression);
        new IDbLiteQueryable<T, T2, T3> Having(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3> Having(Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3> Having(Expression<Func<T, T2, T3, bool>> expression);
        new IDbLiteQueryable<T, T2, T3> Having(string whereString, object parameters = null);
        #endregion

        #region Aggr
        TResult Max<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        TResult Min<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        TResult Sum<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        TResult Avg<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        #endregion

        #region In
        new IDbLiteQueryable<T, T2, T3> In<FieldType>(Expression<Func<T, object>> expression, params FieldType[] inValues);
        new IDbLiteQueryable<T, T2, T3> In<FieldType>(Expression<Func<T, object>> expression, List<FieldType> inValues);
        new IDbLiteQueryable<T, T2, T3> In<FieldType>(Expression<Func<T, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);

        IDbLiteQueryable<T, T2, T3> In<FieldType>(Expression<Func<T,T2, object>> expression, params FieldType[] inValues);
        IDbLiteQueryable<T, T2, T3> In<FieldType>(Expression<Func<T,T2, object>> expression, List<FieldType> inValues);
        IDbLiteQueryable<T, T2, T3> In<FieldType>(Expression<Func<T,T2, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);

        IDbLiteQueryable<T, T2, T3> In<FieldType>(Expression<Func<T, T2,T3, object>> expression, params FieldType[] inValues);
        IDbLiteQueryable<T, T2, T3> In<FieldType>(Expression<Func<T, T2,T3, object>> expression, List<FieldType> inValues);
        IDbLiteQueryable<T, T2, T3> In<FieldType>(Expression<Func<T, T2,T3, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);
        #endregion

        #region Other
        new IDbLiteQueryable<T, T2, T3> Clone();
        new IDbLiteQueryable<T, T2, T3> AS<AsT>(string tableName);
        new IDbLiteQueryable<T, T2, T3> AS(string tableName);
        new IDbLiteQueryable<T, T2, T3> Filter(string FilterName, bool isDisabledGobalFilter = false);
        new IDbLiteQueryable<T, T2, T3> AddParameters(object parameters);
        new IDbLiteQueryable<T, T2, T3> AddParameters(DbLiteParameter[] parameters);
        new IDbLiteQueryable<T, T2, T3> AddParameters(List<DbLiteParameter> parameters);
        new IDbLiteQueryable<T, T2, T3> AddJoinInfo(string tableName, string shortName, string joinWhere, JoinType type = JoinType.Left);
        new IDbLiteQueryable<T, T2, T3> With(string withString);
        new IDbLiteQueryable<T, T2, T3> WithCache(int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3> WithCacheIF(bool isCache, int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2,T3> Distinct();
        bool Any(Expression<Func<T, T2,T3, bool>> expression);
        #endregion
    }
    public partial interface IDbLiteQueryable<T, T2, T3, T4> : IDbLiteQueryable<T>
    {
        #region Where
        new IDbLiteQueryable<T, T2, T3, T4> Where(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4> Where(Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4> Where(Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4> Where(Expression<Func<T, T2, T3, T4, bool>> expression);
        new IDbLiteQueryable<T, T2, T3, T4> Where(List<IConditionalModel> conditionalModels);

        new IDbLiteQueryable<T, T2, T3, T4> WhereIF(bool isWhere, Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4> WhereIF(bool isWhere, Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4> WhereIF(bool isWhere, Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, bool>> expression);

        new IDbLiteQueryable<T, T2, T3, T4> Where(string whereString, object parameters = null);
        new IDbLiteQueryable<T, T2, T3, T4> WhereIF(bool isWhere, string whereString, object parameters = null);

        /// <summary>
        /// if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClass"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T, T2,T3,T4> WhereClass<ClassType>(ClassType whereClass, bool ignoreDefaultValue = false) where ClassType : class, new();
        /// <summary>
        ///  if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClassTypes"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T, T2,T3,T4> WhereClass<ClassType>(List<ClassType> whereClassList, bool ignoreDefaultValue = false) where ClassType : class, new();
        #endregion

        #region Select
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        #endregion

        #region OrderBy
        new IDbLiteQueryable<T, T2, T3, T4> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4> OrderBy(Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4> OrderBy(Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4> OrderBy(Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        new IDbLiteQueryable<T, T2, T3, T4> OrderByIF(bool isOrderBy, string orderFileds);
        new IDbLiteQueryable<T, T2, T3, T4> OrderByIF(bool isOrderBy, Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4> OrderByIF(bool isOrderBy, Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        #endregion

        #region GroupBy
        new IDbLiteQueryable<T, T2, T3, T4> GroupBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4> GroupBy(Expression<Func<T, T2, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4> GroupBy(Expression<Func<T, T2, T3, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4> GroupBy(Expression<Func<T, T2, T3, T4, object>> expression);
        new IDbLiteQueryable<T, T2, T3, T4> Having(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4> Having(Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4> Having(Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4> Having(Expression<Func<T, T2, T3, T4, bool>> expression);
        new IDbLiteQueryable<T, T2, T3, T4> Having(string whereString, object parameters = null);
        #endregion

        #region Aggr
        TResult Max<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        TResult Min<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        TResult Sum<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        TResult Avg<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        #endregion

        #region In
        new IDbLiteQueryable<T, T2, T3, T4> In<FieldType>(Expression<Func<T, object>> expression, params FieldType[] inValues);
        new IDbLiteQueryable<T, T2, T3, T4> In<FieldType>(Expression<Func<T, object>> expression, List<FieldType> inValues);
        new IDbLiteQueryable<T, T2, T3, T4> In<FieldType>(Expression<Func<T, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);

        IDbLiteQueryable<T, T2, T3, T4> In<FieldType>(Expression<Func<T,T2, object>> expression, params FieldType[] inValues);
        IDbLiteQueryable<T, T2, T3, T4> In<FieldType>(Expression<Func<T,T2, object>> expression, List<FieldType> inValues);
        IDbLiteQueryable<T, T2, T3, T4> In<FieldType>(Expression<Func<T,T2, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);

        IDbLiteQueryable<T, T2, T3, T4> In<FieldType>(Expression<Func<T, T2,T3, object>> expression, params FieldType[] inValues);
        IDbLiteQueryable<T, T2, T3, T4> In<FieldType>(Expression<Func<T, T2,T3, object>> expression, List<FieldType> inValues);
        IDbLiteQueryable<T, T2, T3, T4> In<FieldType>(Expression<Func<T, T2,T3, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);

        IDbLiteQueryable<T, T2, T3, T4> In<FieldType>(Expression<Func<T, T2, T3,T4, object>> expression, params FieldType[] inValues);
        IDbLiteQueryable<T, T2, T3, T4> In<FieldType>(Expression<Func<T, T2, T3,T4, object>> expression, List<FieldType> inValues);
        IDbLiteQueryable<T, T2, T3, T4> In<FieldType>(Expression<Func<T, T2, T3,T4, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);
        #endregion

        #region Other
        new IDbLiteQueryable<T, T2, T3, T4> Clone();
        new IDbLiteQueryable<T, T2, T3, T4> AS<AsT>(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4> AS(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4> Filter(string FilterName, bool isDisabledGobalFilter = false);
        new IDbLiteQueryable<T, T2, T3, T4> AddParameters(object parameters);
        new IDbLiteQueryable<T, T2, T3, T4> AddParameters(DbLiteParameter[] parameters);
        new IDbLiteQueryable<T, T2, T3, T4> AddParameters(List<DbLiteParameter> parameters);
        new IDbLiteQueryable<T, T2, T3, T4> AddJoinInfo(string tableName, string shortName, string joinWhere, JoinType type = JoinType.Left);
        new IDbLiteQueryable<T, T2, T3, T4> With(string withString);
        new IDbLiteQueryable<T, T2, T3, T4> WithCache(int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4> WithCacheIF(bool isCache, int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3,T4> Distinct();
        bool Any(Expression<Func<T, T2, T3,T4, bool>> expression);
        #endregion
    }
    public partial interface IDbLiteQueryable<T, T2, T3, T4, T5> : IDbLiteQueryable<T>
    {
        #region Where
        new IDbLiteQueryable<T, T2, T3, T4, T5> Where(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5> Where(Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5> Where(Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5> Where(Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5> Where(Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        new IDbLiteQueryable<T, T2, T3, T4, T5> Where(List<IConditionalModel> conditionalModels);


        new IDbLiteQueryable<T, T2, T3, T4, T5> WhereIF(bool isWhere, Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5> WhereIF(bool isWhere, Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5> WhereIF(bool isWhere, Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, bool>> expression);

        new IDbLiteQueryable<T, T2, T3, T4, T5> Where(string whereString, object parameters = null);
        new IDbLiteQueryable<T, T2, T3, T4, T5> WhereIF(bool isWhere, string whereString, object parameters = null);

        /// <summary>
        /// if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClass"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T, T2, T3, T4, T5> WhereClass<ClassType>(ClassType whereClass, bool ignoreDefaultValue = false) where ClassType : class, new();
        /// <summary>
        ///  if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClassTypes"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T, T2, T3, T4, T5> WhereClass<ClassType>(List<ClassType> whereClassList, bool ignoreDefaultValue = false) where ClassType : class, new();
        #endregion

        #region Select
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, TResult>> expression);
        #endregion

        #region OrderBy
        new IDbLiteQueryable<T, T2, T3, T4, T5> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5> OrderBy(Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5> OrderBy(Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5> OrderBy(Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5> OrderBy(Expression<Func<T, T2, T3, T4, T5, object>> expression, OrderByType type = OrderByType.Asc);
        new IDbLiteQueryable<T, T2, T3, T4, T5> OrderByIF(bool isOrderBy, string orderFileds);
        new IDbLiteQueryable<T, T2, T3, T4, T5> OrderByIF(bool isOrderBy, Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5> OrderByIF(bool isOrderBy, Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, T5, object>> expression, OrderByType type = OrderByType.Asc);
        #endregion

        #region GroupBy
        new IDbLiteQueryable<T, T2, T3, T4, T5> GroupBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5> GroupBy(Expression<Func<T, T2, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5> GroupBy(Expression<Func<T, T2, T3, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5> GroupBy(Expression<Func<T, T2, T3, T4, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5> GroupBy(Expression<Func<T, T2, T3, T4, T5, object>> expression);
        #endregion

        #region Aggr
        TResult Max<TResult>(Expression<Func<T, T2, T3, T4, T5, TResult>> expression);
        TResult Min<TResult>(Expression<Func<T, T2, T3, T4, T5, TResult>> expression);
        TResult Sum<TResult>(Expression<Func<T, T2, T3, T4, T5, TResult>> expression);
        TResult Avg<TResult>(Expression<Func<T, T2, T3, T4, T5, TResult>> expression);
        #endregion

        #region In
        new IDbLiteQueryable<T, T2, T3, T4, T5> In<FieldType>(Expression<Func<T, object>> expression, params FieldType[] inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5> In<FieldType>(Expression<Func<T, object>> expression, List<FieldType> inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5> In<FieldType>(Expression<Func<T, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);
        #endregion

        #region Other
        new IDbLiteQueryable<T, T2, T3, T4, T5> Clone();
        new IDbLiteQueryable<T, T2, T3, T4, T5> AS<AsT>(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5> AS(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5> Filter(string FilterName, bool isDisabledGobalFilter = false);
        new IDbLiteQueryable<T, T2, T3, T4, T5> AddParameters(object parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5> AddParameters(DbLiteParameter[] parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5> AddParameters(List<DbLiteParameter> parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5> AddJoinInfo(string tableName, string shortName, string joinWhere, JoinType type = JoinType.Left);
        new IDbLiteQueryable<T, T2, T3, T4, T5> With(string withString);
        new IDbLiteQueryable<T, T2, T3, T4, T5> WithCache(int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4, T5> WithCacheIF(bool isCache, int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4,T5> Distinct();
        bool Any(Expression<Func<T, T2, T3, T4,T5, bool>> expression);
        #endregion
    }
    public partial interface IDbLiteQueryable<T, T2, T3, T4, T5, T6> : IDbLiteQueryable<T>
    {
        #region Where
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> Where(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> Where(Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> Where(Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> Where(Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> Where(Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> Where(Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> Where(List<IConditionalModel> conditionalModels);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> WhereIF(bool isWhere, Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> WhereIF(bool isWhere, Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> WhereIF(bool isWhere, Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> Where(string whereString, object parameters = null);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> WhereIF(bool isWhere, string whereString, object parameters = null);

        /// <summary>
        /// if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClass"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> WhereClass<ClassType>(ClassType whereClass, bool ignoreDefaultValue = false) where ClassType : class, new();
        /// <summary>
        ///  if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClassTypes"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> WhereClass<ClassType>(List<ClassType> whereClassList, bool ignoreDefaultValue = false) where ClassType : class, new();

        #endregion

        #region Select
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, TResult>> expression);
        #endregion

        #region OrderBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> OrderBy(Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> OrderBy(Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> OrderBy(Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> OrderBy(Expression<Func<T, T2, T3, T4, T5, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression, OrderByType type = OrderByType.Asc);
        new IDbLiteQueryable<T, T2, T3, T4, T5,T6> OrderByIF(bool isOrderBy, string orderFileds);
        new IDbLiteQueryable<T, T2, T3, T4, T5,T6> OrderByIF(bool isOrderBy, Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5,T6> OrderByIF(bool isOrderBy, Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5,T6> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5,T6> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5,T6> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, T5, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, T5,T6, object>> expression, OrderByType type = OrderByType.Asc);
        #endregion

        #region GroupBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> GroupBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> GroupBy(Expression<Func<T, T2, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> GroupBy(Expression<Func<T, T2, T3, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> GroupBy(Expression<Func<T, T2, T3, T4, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> GroupBy(Expression<Func<T, T2, T3, T4, T5, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression);
        #endregion

        #region Aggr
        TResult Max<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, TResult>> expression);
        TResult Min<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, TResult>> expression);
        TResult Sum<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, TResult>> expression);
        TResult Avg<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, TResult>> expression);
        #endregion

        #region In
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> In<FieldType>(Expression<Func<T, object>> expression, params FieldType[] inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> In<FieldType>(Expression<Func<T, object>> expression, List<FieldType> inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> In<FieldType>(Expression<Func<T, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);
        #endregion

        #region Other
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> Clone();
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> AS<AsT>(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> AS(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> Filter(string FilterName, bool isDisabledGobalFilter = false);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> AddParameters(object parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> AddParameters(DbLiteParameter[] parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> AddParameters(List<DbLiteParameter> parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> AddJoinInfo(string tableName, string shortName, string joinWhere, JoinType type = JoinType.Left);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> With(string withString);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> WithCache(int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6> WithCacheIF(bool isCache, int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4, T5,T6> Distinct();
        bool Any(Expression<Func<T, T2, T3, T4, T5,T6, bool>> expression);
        #endregion
    }
    public partial interface IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> : IDbLiteQueryable<T>
    {
        #region Where
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expression);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> Where(List<IConditionalModel> conditionalModels);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> WhereIF(bool isWhere, Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> WhereIF(bool isWhere, Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> WhereIF(bool isWhere, Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expression);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> Where(string whereString, object parameters = null);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> WhereIF(bool isWhere, string whereString, object parameters = null);

        /// <summary>
        /// if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClass"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> WhereClass<ClassType>(ClassType whereClass, bool ignoreDefaultValue = false) where ClassType : class, new();
        /// <summary>
        ///  if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClassTypes"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> WhereClass<ClassType>(List<ClassType> whereClassList, bool ignoreDefaultValue = false) where ClassType : class, new();

        #endregion

        #region Select
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> expression);
        #endregion

        #region OrderBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> OrderBy(Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> OrderBy(Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> OrderBy(Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> OrderBy(Expression<Func<T, T2, T3, T4, T5, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression, OrderByType type = OrderByType.Asc);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6,T7> OrderByIF(bool isOrderBy, string orderFileds);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6,T7> OrderByIF(bool isOrderBy, Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6,T7> OrderByIF(bool isOrderBy, Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6,T7> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6,T7> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6,T7> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, T5, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6,T7> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, T5, T6, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, T5, T6,T7, object>> expression, OrderByType type = OrderByType.Asc);
        #endregion

        #region GroupBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> GroupBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> GroupBy(Expression<Func<T, T2, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> GroupBy(Expression<Func<T, T2, T3, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> GroupBy(Expression<Func<T, T2, T3, T4, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> GroupBy(Expression<Func<T, T2, T3, T4, T5, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression);
        #endregion

        #region Aggr
        TResult Max<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> expression);
        TResult Min<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> expression);
        TResult Sum<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> expression);
        TResult Avg<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> expression);
        #endregion

        #region In
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> In<FieldType>(Expression<Func<T, object>> expression, params FieldType[] inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> In<FieldType>(Expression<Func<T, object>> expression, List<FieldType> inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> In<FieldType>(Expression<Func<T, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);
        #endregion

        #region Other
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> Clone();
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> AS<AsT>(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> AS(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> Filter(string FilterName, bool isDisabledGobalFilter = false);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> AddParameters(object parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> AddParameters(DbLiteParameter[] parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> AddParameters(List<DbLiteParameter> parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> AddJoinInfo(string tableName, string shortName, string joinWhere, JoinType type = JoinType.Left);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> With(string withString);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> WithCache(int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7> WithCacheIF(bool isCache, int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6,T7> Distinct();
        bool Any(Expression<Func<T, T2, T3, T4, T5, T6,T7, bool>> expression);
        #endregion
    }
    public partial interface IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> : IDbLiteQueryable<T>
    {
        #region Where
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> expression);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> Where(List<IConditionalModel> conditionalModels);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool isWhere, Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool isWhere, Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool isWhere, Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> expression);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> Where(string whereString, object parameters = null);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool isWhere, string whereString, object parameters = null);

        /// <summary>
        /// if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClass"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WhereClass<ClassType>(ClassType whereClass, bool ignoreDefaultValue = false) where ClassType : class, new();
        /// <summary>
        ///  if a property that is not empty is a condition
        /// </summary>
        /// <param name="whereClassTypes"></param>
        /// <returns></returns>
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WhereClass<ClassType>(List<ClassType> whereClassList, bool ignoreDefaultValue = false) where ClassType : class, new();

        #endregion                              

        #region Select
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> expression);
        #endregion

        #region OrderBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> OrderBy(Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> OrderBy(Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> OrderBy(Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> OrderBy(Expression<Func<T, T2, T3, T4, T5, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression, OrderByType type = OrderByType.Asc);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7,T8> OrderByIF(bool isOrderBy, string orderFileds);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7,T8> OrderByIF(bool isOrderBy, Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7,T8> OrderByIF(bool isOrderBy, Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7,T8> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7,T8> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7,T8> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, T5, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7,T8> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, T5, T6, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7,T8> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> OrderByIF(bool isOrderBy, Expression<Func<T, T2, T3, T4, T5, T6, T7,T8, object>> expression, OrderByType type = OrderByType.Asc);
        #endregion

        #region GroupBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> GroupBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> GroupBy(Expression<Func<T, T2, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> GroupBy(Expression<Func<T, T2, T3, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> GroupBy(Expression<Func<T, T2, T3, T4, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> GroupBy(Expression<Func<T, T2, T3, T4, T5, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression);
        #endregion

        #region Aggr
        TResult Max<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> expression);
        TResult Min<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> expression);
        TResult Sum<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> expression);
        TResult Avg<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> expression);
        #endregion

        #region In
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> In<FieldType>(Expression<Func<T, object>> expression, params FieldType[] inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> In<FieldType>(Expression<Func<T, object>> expression, List<FieldType> inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> In<FieldType>(Expression<Func<T, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);
        #endregion

        #region Other
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> Clone();
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> AS<AsT>(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> AS(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> Filter(string FilterName, bool isDisabledGobalFilter = false);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> AddParameters(object parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> AddParameters(DbLiteParameter[] parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> AddParameters(List<DbLiteParameter> parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> AddJoinInfo(string tableName, string shortName, string joinWhere, JoinType type = JoinType.Left);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> With(string withString);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WithCache(int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8> WithCacheIF(bool isCache, int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7,T8> Distinct();
        bool Any(Expression<Func<T, T2, T3, T4, T5, T6, T7,T8, bool>> expression);
        #endregion
    }

    #region 9-12
    public partial interface IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> : IDbLiteQueryable<T>
    {
        #region Where
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> expression);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Where(List<IConditionalModel> conditionalModels);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool isWhere, Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool isWhere, Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool isWhere, Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> expression);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Where(string whereString, object parameters = null);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool isWhere, string whereString, object parameters = null);
        #endregion                              

        #region Select
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression);
        #endregion

        #region OrderBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy(Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy(Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy(Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy(Expression<Func<T, T2, T3, T4, T5, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object>> expression, OrderByType type = OrderByType.Asc);
        #endregion

        #region GroupBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy(Expression<Func<T, T2, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy(Expression<Func<T, T2, T3, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy(Expression<Func<T, T2, T3, T4, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy(Expression<Func<T, T2, T3, T4, T5, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object>> expression);
        #endregion

        #region Aggr
        TResult Max<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression);
        TResult Min<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression);
        TResult Sum<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression);
        TResult Avg<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression);
        #endregion

        #region In
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> In<FieldType>(Expression<Func<T, object>> expression, params FieldType[] inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> In<FieldType>(Expression<Func<T, object>> expression, List<FieldType> inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> In<FieldType>(Expression<Func<T, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);
        #endregion

        #region Other
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Clone();
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> AS<AsT>(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> AS(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> Filter(string FilterName, bool isDisabledGobalFilter = false);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> AddParameters(object parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> AddParameters(DbLiteParameter[] parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> AddParameters(List<DbLiteParameter> parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> AddJoinInfo(string tableName, string shortName, string joinWhere, JoinType type = JoinType.Left);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> With(string withString);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> WithCache(int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9> WithCacheIF(bool isCache, int cacheDurationInSeconds = int.MaxValue);
        #endregion                                       
    }
    public partial interface IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IDbLiteQueryable<T>
    {
        #region Where
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> expression);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(List<IConditionalModel> conditionalModels);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool isWhere, Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool isWhere, Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool isWhere, Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> expression);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(string whereString, object parameters = null);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool isWhere, string whereString, object parameters = null);
        #endregion                              

        #region Select
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression);
        #endregion

        #region OrderBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T, T2, T3, T4, T5, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> expression, OrderByType type = OrderByType.Asc);
        #endregion

        #region GroupBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T, T2, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T, T2, T3, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T, T2, T3, T4, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T, T2, T3, T4, T5, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> expression);
        #endregion

        #region Aggr
        TResult Max<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression);
        TResult Min<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression);
        TResult Sum<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression);
        TResult Avg<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression);
        #endregion

        #region In
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> In<FieldType>(Expression<Func<T, object>> expression, params FieldType[] inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> In<FieldType>(Expression<Func<T, object>> expression, List<FieldType> inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> In<FieldType>(Expression<Func<T, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);
        #endregion

        #region Other
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Clone();
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> AS<AsT>(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> AS(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> Filter(string FilterName, bool isDisabledGobalFilter = false);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> AddParameters(object parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> AddParameters(DbLiteParameter[] parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> AddParameters(List<DbLiteParameter> parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> AddJoinInfo(string tableName, string shortName, string joinWhere, JoinType type = JoinType.Left);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> With(string withString);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WithCache(int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10> WithCacheIF(bool isCache, int cacheDurationInSeconds = int.MaxValue);
        #endregion                                       
    }
    public partial interface IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IDbLiteQueryable<T>
    {
        #region Where
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> expression);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(List<IConditionalModel> conditionalModels);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool isWhere, Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool isWhere, Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool isWhere, Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> expression);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(string whereString, object parameters = null);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool isWhere, string whereString, object parameters = null);
        #endregion                              

        #region Select
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression);
        #endregion

        #region OrderBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T, T2, T3, T4, T5, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> expression, OrderByType type = OrderByType.Asc);
        #endregion

        #region GroupBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T, T2, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T, T2, T3, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T, T2, T3, T4, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T, T2, T3, T4, T5, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> expression);
        #endregion

        #region Aggr
        TResult Max<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression);
        TResult Min<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression);
        TResult Sum<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression);
        TResult Avg<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression);
        #endregion

        #region In
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> In<FieldType>(Expression<Func<T, object>> expression, params FieldType[] inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> In<FieldType>(Expression<Func<T, object>> expression, List<FieldType> inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> In<FieldType>(Expression<Func<T, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);
        #endregion

        #region Other
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Clone();
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> AS<AsT>(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> AS(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Filter(string FilterName, bool isDisabledGobalFilter = false);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> AddParameters(object parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> AddParameters(DbLiteParameter[] parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> AddParameters(List<DbLiteParameter> parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> AddJoinInfo(string tableName, string shortName, string joinWhere, JoinType type = JoinType.Left);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> With(string withString);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WithCache(int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WithCacheIF(bool isCache, int cacheDurationInSeconds = int.MaxValue);
        #endregion                                       
    }
    public partial interface IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IDbLiteQueryable<T>
    {
        #region Where
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> expression);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(List<IConditionalModel> conditionalModels);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, Expression<Func<T, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, Expression<Func<T, T2, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, Expression<Func<T, T2, T3, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> expression);

        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(string whereString, object parameters = null);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool isWhere, string whereString, object parameters = null);
        #endregion                              

        #region Select
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression);
        IDbLiteQueryable<TResult> Select<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> expression);
        #endregion

        #region OrderBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T, T2, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T, T2, T3, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T, T2, T3, T4, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T, T2, T3, T4, T5, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> expression, OrderByType type = OrderByType.Asc);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> expression, OrderByType type = OrderByType.Asc);
        #endregion

        #region GroupBy
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T, T2, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T, T2, T3, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T, T2, T3, T4, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T, T2, T3, T4, T5, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> expression);
        IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> expression);
        #endregion

        #region Aggr
        TResult Max<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> expression);
        TResult Min<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> expression);
        TResult Sum<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> expression);
        TResult Avg<TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> expression);
        #endregion

        #region In
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> In<FieldType>(Expression<Func<T, object>> expression, params FieldType[] inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> In<FieldType>(Expression<Func<T, object>> expression, List<FieldType> inValues);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> In<FieldType>(Expression<Func<T, object>> expression, IDbLiteQueryable<FieldType> childQueryExpression);
        #endregion

        #region Other
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Clone();
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> AS<AsT>(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> AS(string tableName);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Filter(string FilterName, bool isDisabledGobalFilter = false);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> AddParameters(object parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> AddParameters(DbLiteParameter[] parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> AddParameters(List<DbLiteParameter> parameters);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> AddJoinInfo(string tableName, string shortName, string joinWhere, JoinType type = JoinType.Left);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> With(string withString);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WithCache(int cacheDurationInSeconds = int.MaxValue);
        new IDbLiteQueryable<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WithCacheIF(bool isCache, int cacheDurationInSeconds = int.MaxValue);
        #endregion                                               
    }
    #endregion
}
