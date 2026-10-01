using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss.BrainOfCthulhu;

public class IchorShower : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.penetrate = -1;
		base.Projectile.Opacity = 1f;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 480;
		base.Projectile.damage = 10;
		base.Projectile.scale = 1f;
		base.Projectile.hostile = true;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(4f, 4f), 170, Vector2.Zero).noGravity = true;
		}
		if (Main.rand.NextBool(8))
		{
			Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(4f, 4f), 228, Vector2.UnitX * base.Projectile.velocity / 10f, 0, default(Color), 0.75f);
		}
		base.Projectile.velocity.Y += 0.075f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(69, 600);
	}
}
