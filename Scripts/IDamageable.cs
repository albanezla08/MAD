using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void on_hit(int damage, Vector3 direction);
    public bool is_alive();
}
