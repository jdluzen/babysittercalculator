using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using XLabs.Forms.Mvvm;

namespace BabysitterCalculator
{
    public abstract class ViewModelModel<T> : ViewModel
    {
        private T model;

        public T Model
        {
            get { return model; } 
            set
            { 
                SetProperty(ref model, value);
            }
        }

        protected ViewModelModel(T model)
        {
            Model = model;
        }

        public static class GenericObjectContainer<TProperty>
        {
            public static readonly Dictionary<string, Func<T, TProperty>> PropGetters;
            public static readonly Dictionary<string, Action<T, TProperty>> PropSetters;

            static GenericObjectContainer()
            {
                PropGetters = new Dictionary<string, Func<T, TProperty>>();
                PropSetters = new Dictionary<string, Action<T, TProperty>>();
            }
        }

        protected bool SetProperty<TProperty>(Expression<Func<TProperty>> propertyExpression, TProperty value, [CallerMemberName] string propertyName = null)
        {
            TProperty prop = GetProperty(propertyExpression);
            if (/*prop == value || */EqualityComparer<TProperty>.Default.Equals(prop, value))
                return false;
            Action<T, TProperty> setter;
            if (!GenericObjectContainer<TProperty>.PropSetters.TryGetValue(propertyName, out setter))
            {
                MemberExpression me = (MemberExpression)propertyExpression.Body;
                PropertyInfo pi = (PropertyInfo)me.Member;
                ParameterExpression paInstance = Expression.Parameter(pi.DeclaringType);
                ParameterExpression peArg = Expression.Parameter(typeof(TProperty));
                MethodCallExpression mce = Expression.Call(paInstance, pi.SetMethod, peArg);
                GenericObjectContainer<TProperty>.PropSetters[propertyName] = setter = Expression.Lambda<Action<T, TProperty>>(mce, paInstance, peArg).Compile();
            }
            setter(Model, value);
            NotifyPropertyChanged(propertyName);
            return true;
        }

        protected TProperty GetProperty<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            Func<T, TProperty> getter;
            string propertyName = ((MemberExpression)propertyExpression.Body).Member.Name;
            if (!GenericObjectContainer<TProperty>.PropGetters.TryGetValue(propertyName, out getter))
            {
                MemberExpression me = (MemberExpression)propertyExpression.Body;
                PropertyInfo pi = (PropertyInfo)me.Member;
                ParameterExpression paInstance = Expression.Parameter(pi.DeclaringType);
                ParameterExpression peArg = Expression.Parameter(typeof(TProperty));
                MethodCallExpression mce = Expression.Call(paInstance, pi.GetMethod);
                GenericObjectContainer<TProperty>.PropGetters[propertyName] = getter = Expression.Lambda<Func<T, TProperty>>(mce, paInstance).Compile();
            }
            return getter(Model);
        }
    }
}
