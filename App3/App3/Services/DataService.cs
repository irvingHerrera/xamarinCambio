namespace App3.Services
{
    using App3.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    

    public class DataService
    {
        public bool DeleteAll<T>() where T : class
        {
            try
            {
                using (var da = new DataAcces())
                {
                    var oldRecords = da.GetList<T>(false);
                    foreach (var oldRecord in oldRecords)
                    {
                        da.Delete(oldRecord);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public T DeleteAllAndInsert<T>(T model) where T : class
        {
            try
            {
                using (var da = new DataAcces())
                {
                    var oldRecords = da.GetList<T>(false);
                    foreach (var oldRecord in oldRecords)
                    {
                        da.Delete(oldRecord);
                    }

                    da.Insert(model);

                    return model;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return model;
            }
        }

        public T InsertOrUpdate<T>(T model) where T : class
        {
            try
            {
                using (var da = new DataAcces())
                {
                    var oldRecord = da.Find<T>(model.GetHashCode(), false);
                    if (oldRecord != null)
                    {
                        da.Update(model);
                    }
                    else
                    {
                        da.Insert(model);
                    }

                    return model;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return model;
            }
        }

        public T Insert<T>(T model)
        {
            using (var da = new DataAcces())
            {
                da.Insert(model);
                return model;
            }
        }

        public T Find<T>(int pk, bool withChildren) where T : class
        {
            using (var da = new DataAcces())
            {
                return da.Find<T>(pk, withChildren);
            }
        }

        public T First<T>(bool withChildren) where T : class
        {
            using (var da = new DataAcces())
            {
                return da.GetList<T>(withChildren).FirstOrDefault();
            }
        }

        public List<T> Get<T>(bool withChildren) where T : class
        {
            using (var da = new DataAcces())
            {
                return da.GetList<T>(withChildren).ToList();
            }
        }

        public void Update<T>(T model)
        {
            using (var da = new DataAcces())
            {
                da.Update(model);
            }
        }

        public void Delete<T>(T model)
        {
            using (var da = new DataAcces())
            {
                da.Delete(model);
            }
        }

        public void Save<T>(List<T> list) where T : class
        {
            using (var da = new DataAcces())
            {
                foreach (var record in list)
                {
                    InsertOrUpdate(record);
                }
            }
        }
    }
}
