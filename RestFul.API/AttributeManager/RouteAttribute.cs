using System;

namespace RestFul.API.AttributeManager
{
    /// <summary>
    /// 标注地址Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class RouteAttribute : Attribute
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 提交方式
        /// </summary>
        public string Verbs { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Summary { get; set; }

        public RouteAttribute(string path) : this(path, null) 
        { 
        }

        public RouteAttribute(string path, string verbs)
        {
            this.Path = path;
            this.Verbs = verbs;
        }

        protected bool Equals(RouteAttribute other)
        {
            return base.Equals(other)
                && string.Equals(Path, other.Path)
                && string.Equals(Verbs, other.Verbs);
                //&& string.Equals(Summary, other.Summary);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((RouteAttribute)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Path != null ? Path.GetHashCode() : 0);
                //hashCode = (hashCode * 397) ^ (Summary != null ? Summary.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Verbs != null ? Verbs.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
