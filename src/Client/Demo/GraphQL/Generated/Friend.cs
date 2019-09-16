﻿using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace StrawberryShake.Client
{
    public class Friend
        : IFriend
    {
        public Friend(
            IReadOnlyList<IHasName> nodes)
        {
            nodes = Nodes;
        }
        public IReadOnlyList<IHasName> Nodes { get; }
    }
}
