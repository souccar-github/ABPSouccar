﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Project.Souccar.Core;
using Project.Souccar.Core.DesignByContract;
using Project.Souccar.Core.Extensions;

namespace Project.Souccar.Domain.DomainModel
{
    /// <summary>
    ///     Provides a standard base class for facilitating comparison of value objects using all the object's properties.
    /// 
    ///     For a discussion of the implementation of Equals/GetHashCode, see 
    ///     http://devlicio.us/blogs/billy_mccafferty/archive/2007/04/25/using-equals-gethashcode-effectively.aspx
    ///     and http://groups.google.com/group/sharp-architecture/browse_thread/thread/f76d1678e68e3ece?hl=en for 
    ///     an in depth and conclusive resolution.
    /// </summary>
    [Serializable]
    public abstract class ValueObject : BaseObject
    {
        public static bool operator ==(ValueObject valueObject1, ValueObject valueObject2)
        {
            if ((object)valueObject1 == null)
            {
                return (object)valueObject2 == null;
            }

            return valueObject1.Equals(valueObject2);
        }

        public static bool operator !=(ValueObject valueObject1, ValueObject valueObject2)
        {
            return !(valueObject1 == valueObject2);
        }

        public override bool Equals(object obj)
        {
            // We overrided this method because the base comparing is not working.
            if (obj == null)
                return false;
            if (obj.GetType() != GetType())
                return false;
            // Compare all public properties
            var publicProperties = GetType().GetProperties().Where(p => !p.IsCollectionProperty());
            return publicProperties
                    .All(item => item.GetValue(this, null) != null ?
                    item.GetValue(this, null).Equals(item.GetValue(obj, null)) : item.GetValue(obj, null) == null);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        ///     The getter for SignatureProperties for value objects should include the properties 
        ///     which make up the entirety of the object's properties; that's part of the definition 
        ///     of a value object.
        /// </summary>
        /// <remarks>
        ///     This ensures that the value object has no properties decorated with the 
        ///     [DomainSignature] attribute.
        /// </remarks>
        protected override IEnumerable<PropertyInfo> GetTypeSpecificSignatureProperties()
        {
            IEnumerable<PropertyInfo> invalidlyDecoratedProperties =
                GetType().GetProperties().Where(
                    p => Attribute.IsDefined(p, typeof(DomainSignatureAttribute), true));

            string message = "Properties were found within " + GetType() +
                             @" having the
                [DomainSignature] attribute. The domain signature of a value object includes all
                of the properties of the object by convention; consequently, adding [DomainSignature]
                to the properties of a value object's properties is misleading and should be removed. 
                Alternatively, you can inherit from Entity if that fits your needs better.";

            Check.Require(
                !invalidlyDecoratedProperties.Any(),
                message);

            return GetType().GetProperties();
        }
    }
}