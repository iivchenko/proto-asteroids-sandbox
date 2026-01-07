using Engine.EFS;
using Engine.EFS.Systems;
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
           .Where(face => face is not null)
           .Where(face => face is IPlayerFace)
           .Cast<IPlayerFace>()
           .Iter(face =>
           {
               if (_keyboardService.IsKeyDown(Keys.ArrowLeft))
               {
                   var angularVelocity = face.AngularVelocity - face.MaxAngularAcceleration;
                   face.AngularVelocity = Math.Abs(angularVelocity) > face.MaxRotation ? -face.MaxRotation : angularVelocity;

                   face.Rotation += face.AngularVelocity * delta;
               }
               else if (_keyboardService.IsKeyDown(Keys.ArrowRight))
               {
                   var angularVelocity = face.AngularVelocity + face.MaxAngularAcceleration;
                   face.AngularVelocity = Math.Abs(angularVelocity) > face.MaxRotation ? face.MaxRotation : angularVelocity;

                   face.Rotation += face.AngularVelocity * delta;
               }
               else
               {
                   face.AngularVelocity = 0;
               }
           });
    }
}