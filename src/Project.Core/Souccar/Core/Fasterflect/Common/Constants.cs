﻿#region License

// Copyright 2010 Buu Nguyen, Morten Mertner
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://fasterflect.codeplex.com/

#endregion

using System;
using System.Reflection;
using Project.Souccar.Core.Fasterflect.Emitter;

namespace Project.Souccar.Core.Fasterflect
{
    internal static class Constants
    {
        public const string IndexerSetterName = "set_Item";
        public const string IndexerGetterName = "get_Item";
        public const string ArraySetterName = "[]=";
        public const string ArrayGetterName = "=[]";
        public static readonly Type ObjectType = typeof(object);
        public static readonly Type IntType = typeof(int);
        public static readonly Type StructType = typeof(ValueTypeHolder);
        public static readonly Type VoidType = typeof(void);
        public static readonly Type[] ArrayOfObjectType = new[] { typeof(object) };
        public static readonly object[] EmptyObjectArray = new object[0];
        public static readonly string[] EmptyStringArray = new string[0];
        public static readonly PropertyInfo[] EmptyPropertyInfoArray = new PropertyInfo[0];
    }
}
