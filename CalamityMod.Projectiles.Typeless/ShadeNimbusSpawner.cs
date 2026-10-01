using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class ShadeNimbusSpawner : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/NPCs/HiveMind/DankCreeper";

	public ref float EffectStrength => ref base.Projectile.ai[2];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 70);
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 35;
	}

	public override void AI()
	{
		base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath1, base.Projectile.Center);
		for (int k = 0; k < 20; k++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 13, (base.Projectile.velocity.X > 0f) ? 1f : (-1f), -1f);
		}
		if (!Main.dedServ)
		{
			Vector2 goreVelocity = default(Vector2);
			((Vector2)(ref goreVelocity))._002Ector(base.Projectile.velocity.X, base.Projectile.velocity.Y * 0.4f);
			Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position, goreVelocity, base.Mod.Find<ModGore>("DankCreeperGore").Type);
			Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position, goreVelocity, base.Mod.Find<ModGore>("DankCreeperGore2").Type);
			Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position, goreVelocity, base.Mod.Find<ModGore>("DankCreeperGore3").Type);
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			int cloudAmt = ((EffectStrength == 3f) ? 5 : ((EffectStrength == 2f) ? 3 : 2));
			for (int c = -(cloudAmt - 1) / 2; c <= (cloudAmt - 1) / 2; c++)
			{
				Vector2 cloudVelocity = ((c == 0) ? Vector2.Zero : (Vector2.UnitX.RotatedByRandom(0.04363323375582695) * Main.rand.NextFloat(3f, 9.5f)));
				cloudVelocity *= (((float)c < 0f) ? (-1f) : 1f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, cloudVelocity, ModContent.ProjectileType<ShadeNimbus>(), base.Projectile.damage, 0f, Main.myPlayer);
			}
		}
	}
}
