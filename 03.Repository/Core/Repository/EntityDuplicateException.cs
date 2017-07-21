﻿using System;

namespace Core.Repository
{
    [Serializable]
    public class EntityDuplicateException : EntityException
    {
        public override int InternalExceptionCode
        {
            get
            {
                return CoreException.EntityDuplicateExpectionCode;
            }
        }

        public EntityDuplicateException(object entity, string message) : base(entity, message)
        {
        }

        public EntityDuplicateException(object entity, string message, Exception inner) : base(entity, message, inner)
        {
        }
    }
}
