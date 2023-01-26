using Cakewalk.IoC.Core;
using UnityEngine;

public class BootStrapper : BaseBootStrapper
{
    public override void Configure(Container _container)
    {
        _container.Register<PlayerHUD>();
    }
}
