﻿using Engine.Entities;
using Engine.Graphics;
using Engine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Engine.Particles
{
    public sealed class ParticleEngine : IUpdatable, IDrawable
    {
        private readonly IList<Particle> _particles;
        private readonly Action<Random, float, Particle> _update;
        private readonly IPainter _painter;
        private readonly Action<float> _onFinish;
        private readonly Random _random;

        private bool _isFinished = false;

        public ParticleEngine(
            IEnumerable<Particle> particles,
            Action<Random, float, Particle> update,
            int seed,
            IPainter painter,
            Action<float> onFinish)
        {
            _particles = particles.ToList();
            _update = update;
            _painter = painter;
            _onFinish = onFinish;

            _random = new Random(seed);
        }

        public IEnumerable<string> Tags => Enumerable.Empty<string>();

        public void Update(float time)
        {
            if (!_isFinished)
            {
                _particles
                .Where(particle => particle.TTL > 0)
                .Iter(particle => _update(_random, time, particle));

                if (_particles.All(x => x.TTL <= 0))
                {
                    _isFinished = true;
                    _onFinish(time);
                }
            }            
        }

        public void Draw(float time)
        {
            _particles
                .Where(particle => particle.TTL > 0)
                .Iter(particle => 
                        _painter.Draw(
                            particle.Sprite, 
                            particle.Position, 
                            Vector2.Zero, 
                            particle.Scale, 
                            particle.Angle, 
                            particle.Color));
        }
    }
}
