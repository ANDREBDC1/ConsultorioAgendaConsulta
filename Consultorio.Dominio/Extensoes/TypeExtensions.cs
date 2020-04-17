﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Consultorio.Dominio.Extensoes
{
    public static class TypeExtensions
    {
        public static List<T> ObterDiferentesPor<T>(this List<T> lista, List<T> listaDeComparacao,
            Func<T, T, bool> funcaoDeComparacao) where T : class
        {
            return lista.Where(elemntoDaLista =>
            {
                var compracao = listaDeComparacao
                    .FirstOrDefault(elemntoDeComparacao => funcaoDeComparacao(elemntoDaLista, elemntoDeComparacao));

                if (compracao == null)
                    return false;

                return !compracao.PublicInstancePropertiesEqual(elemntoDaLista);

            })
            .ToList();
        }

        public static bool PublicInstancePropertiesEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self == null || to == null)
                return self == to;

            var type = typeof(T);
            var ignoreList = new List<string>(ignore);
            var unequalProperties =
                from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where !ignoreList.Contains(pi.Name) && pi.GetUnderlyingType().IsSimpleType() && pi.GetIndexParameters().Length == 0
                let selfValue = type.GetProperty(pi.Name)?.GetValue(self, null)
                let toValue = type.GetProperty(pi.Name)?.GetValue(to, null)
                where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                select selfValue;

            return !unequalProperties.Any();
        }

        /// <summary>
        /// Determine whether a type is simple (String, Decimal, DateTime, etc) 
        /// or complex (i.e. custom class with public properties and methods).
        /// </summary>
        /// <see cref="http://stackoverflow.com/questions/2442534/how-to-test-if-type-is-primitive"/>
        public static bool IsSimpleType(
           this Type type)
        {
            return
               type.IsValueType ||
               type.IsPrimitive ||
               new[]
               {
               typeof(string),
               typeof(decimal),
               typeof(DateTime),
               typeof(DateTimeOffset),
               typeof(TimeSpan),
               typeof(Guid)
               }.Contains(type) ||
               Convert.GetTypeCode(type) != TypeCode.Object;
        }

        public static Type GetUnderlyingType(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                default:
                    throw new ArgumentException
                    (
                       "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }
    }
}
