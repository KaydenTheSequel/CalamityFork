using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MortalityBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.White;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		if (!Main.dedServ)
		{
			for (int i = -1; i <= 1; i += 2)
			{
				Vector2 rotatedOffset = Utils.RotatedBy(new Vector2((float)Math.Sin(Time / 45f * ((float)Math.PI * 2f)) * 10f * (float)i, 2f), (double)(base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f), default(Vector2));
				Vector2 position = base.Projectile.Center + rotatedOffset;
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, 261, null, 0, newColor);
				dust.color = Main.hslToRgb(Main.rand.NextFloat(), 0.9f, 0.5f);
				dust.velocity = Vector2.Zero;
				dust.scale = 1.5f;
				dust.noGravity = true;
			}
		}
		Time++;
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1000f, Main.player[base.Projectile.owner], ignoreTiles: false);
		if (potentialTarget != null)
		{
			base.Projectile.velocity = (base.Projectile.velocity * 5f + base.Projectile.SafeDirectionTo(potentialTarget.Center) * 13f) / 6f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 30);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			float redHue = Main.rgbToHsl(Color.Red).X;
			float greenHue = Main.rgbToHsl(Color.SeaGreen).X;
			float blueHue = Main.rgbToHsl(Color.Blue).X;
			Vector2 spawnPosition = base.Projectile.Center + Main.rand.NextVector2CircularEdge(10f, 10f);
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), spawnPosition, Main.rand.NextVector2CircularEdge(9f, 9f), ModContent.ProjectileType<MortalityBolt>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner).localAI[0] = Utils.SelectRandom<float>(Main.rand, redHue, greenHue, blueHue);
		}
	}
}
