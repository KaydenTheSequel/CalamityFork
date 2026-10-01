using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.Projectiles.Rogue;

public class ConsecratedWaterProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/ConsecratedWater";

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 34;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 200;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 0;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 75f && base.Projectile.velocity.Y < 10f)
		{
			base.Projectile.velocity.Y += 0.15f;
		}
		base.Projectile.rotation += MathHelper.ToRadians(((Vector2)(ref base.Projectile.velocity)).Length());
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		Vector2 dspeed = default(Vector2);
		for (int i = 0; i < 30; i++)
		{
			((Vector2)(ref dspeed))._002Ector(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f));
			Dust.NewDust(base.Projectile.Center, 1, 1, 68, dspeed.X, dspeed.Y, 0, default(Color), 1.1f);
		}
		SoundEngine.PlaySound(in SoundID.Item107, base.Projectile.Bottom);
		if (base.Projectile.ai[1] == 0f)
		{
			if (WorldUtils.Find(base.Projectile.Top.ToTileCoordinates(), Searches.Chain(new Searches.Down(80), new Conditions.IsSolid()), out var result))
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), result.ToVector2() * 16f, Vector2.Zero, ModContent.ProjectileType<BlueFlamePillar>(), base.Projectile.damage, 2f, base.Projectile.owner);
			}
		}
		else
		{
			if (base.Projectile.ai[1] != 1f)
			{
				return;
			}
			for (float i2 = -1f; i2 <= 1f; i2++)
			{
				if (WorldUtils.Find((base.Projectile.Top + i2 * Main.rand.NextFloat(56f, 108f) * Vector2.UnitX).ToTileCoordinates(), Searches.Chain(new Searches.Down(80), new Conditions.IsSolid()), out var result2))
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), result2.ToVector2() * 16f, Vector2.Zero, ModContent.ProjectileType<BlueFlamePillar>(), base.Projectile.damage, 2f, base.Projectile.owner);
				}
			}
		}
	}
}
