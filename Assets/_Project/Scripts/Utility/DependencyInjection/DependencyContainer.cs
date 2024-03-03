using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Utility.DependencyInjection
{
    public class DependencyContainer
    {
        #region FIELDS PRIVATE
        private List<AbstractDependency> _dependencies = new();
        #endregion

        #region METHODS PRIVATE
        private MethodInfo CreateGenericMethod(Type parameterType, string name, Type[] argumentTypes)
        {
            var type = typeof(DependencyContainer);
            var flags = BindingFlags.DeclaredOnly
                | BindingFlags.Instance
                | BindingFlags.Public;
            var method = type.GetMethod(name, flags, null, argumentTypes, null);
            var typeArgs = new Type[1] { parameterType };
            return method.MakeGenericMethod(typeArgs);
        }
        #endregion

        #region METHODS PUBLIC
        public Dependency<T> Bind<T>()
        {
            Dependency<T> dependency = null;
            var dependencies = _dependencies.OfType<Dependency<T>>().ToList();
            if(dependencies.Count == 0)
            {
                dependency = new Dependency<T>();
            }
            else
            {
                dependency = dependencies.Find(e => e.ID is null);
                if(dependency != null)
                {
                    _dependencies.Remove(dependency);
                    dependency = new Dependency<T>();
                }
            }

            _dependencies.Add(dependency);
            return dependency;
        }

        public Dependency<T> Bind<T>(string id)
        {
            Dependency<T> dependency = null;
            var dependencies = _dependencies.OfType<Dependency<T>>().ToList();
            if (dependencies.Count == 0)
            {
                dependency = new Dependency<T>(id);
            }
            else
            {
                dependency = dependencies.Find(e => e.ID == id);
                if (dependency != null)
                {
                    _dependencies.Remove(dependency);
                }

                dependency = new Dependency<T>(id);
            }

            _dependencies.Add(dependency);
            return dependency;
        }

        public T Get<T>()
        {
            Dependency<T> dependency = null;
            var dependencies = _dependencies.OfType<Dependency<T>>().ToList();
            if (dependencies.Count == 0)
            {
                throw new ArgumentException("Type is not a dependecy: " + typeof(T).FullName);
            }

            dependency = dependencies.Find(e => e.ID is null);
            if (dependency is null)
            {
                throw new ArgumentException("Type is not a dependecy: " + typeof(T).FullName);
            }

            return dependency.Instance;
        }

        public T Get<T>(string id)
        {
            Dependency<T> dependency = null;
            var dependencies = _dependencies.OfType<Dependency<T>>().ToList();
            if (dependencies.Count == 0)
            {
                throw new ArgumentException("Type is not a dependecy: " + typeof(T).FullName);
            }

            dependency = dependencies.Find(e => e.ID == id);
            if (dependency is null)
            {
                throw new ArgumentException("Type is not a dependecy: " + typeof(T).FullName);
            }

            return dependency.Instance;
        }

        public object Inject(object dependant)
        {
            var flags = BindingFlags.DeclaredOnly
                | BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.NonPublic;

            var type = dependant.GetType();
            while (type != null)
            {
                var fields = type.GetFields(flags);
                foreach (var field in fields)
                {
                    var attibute = field.GetCustomAttribute<InjectAttribute>(false);
                    if (attibute is null) continue;

                    var argumentTypes = attibute.ID is null ? Type.EmptyTypes : new Type[1] { typeof(string) };
                    var method = CreateGenericMethod(field.FieldType, nameof(Get), argumentTypes);
                    var parameters = attibute.ID is null ? null : new object[1] { attibute.ID };
                    field.SetValue(dependant, method.Invoke(this, parameters));
                }

                type = type.BaseType;
            }

            return dependant;
        }
        #endregion
    }
}
