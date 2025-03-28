﻿namespace Engine.EFS.Faces;

public interface IUpdatable
{
    int Position { get; set; }
    void Update(float delta);
}
