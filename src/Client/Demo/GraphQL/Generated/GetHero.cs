﻿using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace StrawberryShake.Client
{
    public class GetHero
        : IGetHero
    {
        public GetHero(
            IHero hero)
        {
            hero = Hero;
        }
        public IHero Hero { get; }
    }
}
