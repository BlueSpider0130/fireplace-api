﻿using FireplaceApi.Core.Enums;
using FireplaceApi.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireplaceApi.Core.ValueObjects
{
    public class Identifier
    {
        private long? _id;
        private string _name;

        public long? Id 
        {
            get { return _id; }
            set 
            { 
                if(!value.HasValue)
                    throw new ArgumentNullException(nameof(value));
                _id = value; 
                State = IdentifierState.HasBoth; 
            }
        }
        public string Name
        {
            get { return _name; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(value));
                _name = value;
                State = IdentifierState.HasBoth; 
            }
        }
        public IdentifierState State { get; private set; }

        public Identifier(long id)
        {
            Id = id;
            State = IdentifierState.HasId;
        }

        public Identifier(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            State = IdentifierState.HasName;
        }

        public Identifier(long? id, string name)
        {
            var nameHasValue = !string.IsNullOrWhiteSpace(name);
            if (id.HasValue && nameHasValue)
            {
                State = IdentifierState.HasBoth;
                Id = id;
                Name = name;
            }
            else if(id.HasValue)
            {
                Id = id;
                State = IdentifierState.HasId;
            }
            else if(nameHasValue)
            {
                Name = name;
                State = IdentifierState.HasName;
            }
            else
            {
                throw new ArgumentException(
                    $"One of id or name should have a value. {new { id, name}.ToJson()}");
            }
        }
    }

    public enum IdentifierState
    {
        HasId,
        HasName,
        HasBoth
    }
}