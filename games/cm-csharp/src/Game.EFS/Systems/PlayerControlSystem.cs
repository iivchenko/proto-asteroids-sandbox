using Engine;
using Engine.EFS;
using Engine.EFS.Faces;
using Engine.EFS.Systems;
using Engine.Math;
using Engine.Services.Keyboard;
using Game.EFS.Entities;
using Game.EFS.Faces;

namespace Game.EFS.Systems;

public sealed class PlayerControlSystem(
    IKeyboardService keyboardService,
    IEntityBuilderFactory<ProjectileBuilder> projectileBuilderFactory
    ) : ISystem
{
    private const float LaserCooldown = 0.5f;
    private readonly IKeyboardService _keyboardService = keyboardService;
    private readonly IEntityBuilderFactory<ProjectileBuilder> _projectileBuilderFactory = projectileBuilderFactory;

    public IEnumerable<IWorldCommand> Process(IEnumerable<IEntity> faces, float delta)
    {
        return
        faces
           .Where(entity => entity is not null)
           .Where(entity => entity is IPlayerFace && entity is IMovableFace)
           .SelectMany(entity =>
           {
               var commands = new List<IWorldCommand>();
               var player = (IPlayerFace)entity;
               var movable = (IMovableFace)entity;

               if (_keyboardService.IsKeyDown(Keys.ArrowLeft))
               {
                   var angularVelocity = player.AngularVelocity - player.MaxAngularAcceleration;
                   player.AngularVelocity = Angle.Max(player.MaxRotation * -1, angularVelocity);

                   movable.RotationVelocity = player.AngularVelocity;

               }
               else if (_keyboardService.IsKeyDown(Keys.ArrowRight))
               {
                   var angularVelocity = player.AngularVelocity + player.MaxAngularAcceleration;
                   player.AngularVelocity = Angle.Min(player.MaxRotation, angularVelocity);

                   movable.RotationVelocity = player.AngularVelocity;
               }
               else
               {
                   movable.RotationVelocity = Angle.Zero;
                   player.AngularVelocity = Angle.Zero;
               }

               if (_keyboardService.IsKeyDown(Keys.ArrowUp))
               {
                   var direction = movable.Rotation.ToVector().Normalize();
                   var velocity = movable.LinearVelocity + direction * player.MaxAcceleration;

                   movable.LinearVelocity = velocity.Length() > player.MaxSpeed ? velocity.Normalize() * player.MaxSpeed : velocity;
               }

               if (_keyboardService.IsKeyDown(Keys.Space) && player.LaserCooldown <= 0.0f)
               {
                   var projectile =
                    _projectileBuilderFactory
                        .Create()
                        .WithPosition(movable.Position)
                        .WithRotation(movable.Rotation)
                        .WithDirection(movable.Rotation.ToVector().Normalize())
                        .Build();

                   player.LaserCooldown = LaserCooldown;

                   commands.Add(new AddEntityCommand(projectile));
               }
               else if (player.LaserCooldown > 0)
               {
                   player.LaserCooldown -= delta;
               }

               return commands;
           });
    }
}