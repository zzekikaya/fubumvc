﻿using System.Collections.Generic;
using System.Linq;
using FubuCore;

namespace FubuValidation
{
    public class ValidationProvider : IValidationProvider
    {
        private readonly IEnumerable<IValidationSource> _sources;
        private readonly ITypeResolver _typeResolver;

        public ValidationProvider(ITypeResolver typeResolver, IEnumerable<IValidationSource> sources)
        {
            _typeResolver = typeResolver;
            _sources = sources;
        }

        public Notification Validate(object target)
        {
            var validatedType = _typeResolver.ResolveType(target);
            var notification = new Notification(validatedType);
            Validate(target, notification);
            return notification;
        }

        public void Validate(object target, Notification notification)
        {
            var validatedType = _typeResolver.ResolveType(target);
            var context = new ValidationContext(this, notification, target){
                TargetType = validatedType
            };

            _sources.SelectMany(x => x.RulesFor(validatedType))
                .Each(rule => rule.Validate(context));
        }
    }
}