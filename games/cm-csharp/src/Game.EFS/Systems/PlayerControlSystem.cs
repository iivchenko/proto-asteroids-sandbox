using Engine;
using Engine.EFS;
using Engine.EFS.Faces;
using Engine.EFS.Systems;
using Engine.Math;
using Engine.Services.Keyboard;
using Engine.Utilities;
using Game.EFS.Faces;

namespace Game.EFS.Systems;

public sealed class PlayerControlSystem(IKeyboardService keyboardService) : ISystem
{
    private readonly IKeyboardService _keyboardService = keyboardService;

    public void Process(IEnumerable<IEntity> faces, float delta)
    {
        faces
           .Where(entity => entity is not null)
           .Where(entity => entity is IPlayerFace && entity is IMovableFace)
           .Iter(entity =>
           {
               var player = (IPlayerFace)entity;
               var movable = (IMovableFace)entity;

               if (_keyboardService.IsKeyDown(Keys.ArrowLeft))
               {
                   var angularVelocity = player.AngularVelocity - player.MaxAngularAcceleration;
                   player.AngularVelocity = Angle.Max(player.MaxRotation * -1, angularVelocity);//  Angle. Math.Abs(angularVelocity) > player.MaxRotation ? -player.MaxRotation : angularVelocity;

                   movable.RotationVelocity = player.AngularVelocity;

               }
               else if (_keyboardService.IsKeyDown(Keys.ArrowRight))
               {
                   var angularVelocity = player.AngularVelocity + player.MaxAngularAcceleration;
                   player.AngularVelocity = Angle.Min(player.MaxRotation, angularVelocity); //Math.Abs(angularVelocity) > player.MaxRotation ? player.MaxRotation : angularVelocity;

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
           });
    }
}