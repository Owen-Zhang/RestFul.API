using System;
namespace RestFul.API.Interface
{
    public interface IRestGetWay<T>
    {
        object Get(T request);
        object Post(T request);
        object Put(T request);
        object Delete(T request);
    }

    public class RestService<T> : IRestGetWay<T>
    {
        public virtual Object OnGet(T request) { throw new NotImplementedException("please implement The Method OnGet"); }
        public virtual Object OnPost(T request) { throw new NotImplementedException("please implement The Method OnPost"); }
        public virtual Object OnPut(T request) { throw new NotImplementedException("please implement The Method OnPut"); }
        public virtual Object OnDelete(T request) { throw new NotImplementedException("please implement The Method OnDelete"); }

        public object Get(T request)
        {
            try
            {
                OnBeforeExecute(request);
                var result = OnGet(request);
                OnEndExecute(request, result);
                return result;
            }
            catch (Exception e)
            { 
                var exception = HandleException(request, e);
                return exception;
            }
        }

        public object Post(T request)
        {
            try
            {
                OnBeforeExecute(request);
                var result = OnPost(request);
                OnEndExecute(request, result);
                return result;
            }
            catch (Exception e)
            {
                var exception = HandleException(request, e);
                return exception;
            }
        }

        public object Put(T request)
        {
            try
            {
                OnBeforeExecute(request);
                var result = OnPut(request);
                OnEndExecute(request, result);
                return result;
            }
            catch (Exception e)
            {
                var exception = HandleException(request, e);
                return exception;
            }
        }

        public object Delete(T request)
        {
            try
            {
                OnBeforeExecute(request);
                var result = OnDelete(request);
                OnEndExecute(request, result);
                return result;
            }
            catch (Exception e)
            {
                var exception = HandleException(request, e);
                return exception;
            }
        }

        /// <summary>
        /// 用时最好要调用基类的方法
        /// </summary>
        protected virtual void OnBeforeExecute(T request) {}

        protected virtual void OnEndExecute(T request, object response) { }

        protected virtual object HandleException(T request, Exception e)
        {
            /*这里可以构造错误信息*/
            return null;
        }

    }
}
