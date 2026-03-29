using System.Collections.Generic;
using Entities;

public class EntityWorld
{
    public HashSet<Enemy> Enemies { get; } = new();
    public HashSet<Projectile> Projectiles { get; } = new();
}