using System;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(false)]
public class EclipsesStealth : ModProjectile, ILocalizedModType, IModType
{
	public const float RainDamageMult = 0.2f;

	public const float ExplosionDamageMult = 0.5f;

	private bool changedTimeLeft;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/EclipsesFall";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.timeLeft = 150 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 1f, 0.8f, 0.3f);
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
			if (Main.rand.NextBool(5))
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Vector2.UnitY.RotatedBy(base.Projectile.rotation) * Main.rand.NextFloat(-16f, 16f), scale: Main.rand.NextFloat(0.8f, 1.2f), color: Main.rand.NextBool() ? Color.Indigo : Color.DarkOrange, velocity: base.Projectile.velocity * 0.2f, affectedByGravity: false, lifetime: 60));
			}
		}
		else
		{
			if (!changedTimeLeft)
			{
				base.Projectile.MaxUpdates = 1;
				base.Projectile.timeLeft = 600;
				changedTimeLeft = true;
			}
			if (base.Projectile.owner == Main.myPlayer)
			{
				base.Projectile.localAI[1]--;
				if (base.Projectile.localAI[1] <= 0f)
				{
					base.Projectile.localAI[1] = Main.rand.Next(8, 11);
					int type = ModContent.ProjectileType<EclipsesSmol>();
					Vector2 spawnPos = base.Projectile.Center - new Vector2(Main.rand.NextFloat(-100f, 100f), Main.rand.NextFloat(500f, 800f));
					Vector2 spearVel = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(spawnPos, Main.npc[(int)base.Projectile.ai[1]], 29f, 2) + Vector2.UnitX * Main.rand.NextFloat(-6f, 6f);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPos, spearVel, type, (int)((float)base.Projectile.damage * 0.2f), base.Projectile.knockBack * 0.2f, base.Projectile.owner);
				}
			}
		}
		base.Projectile.StickyProjAI(10);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		base.Projectile.ModifyHitNPCSticky(1);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
		{
			((Rectangle)(ref targetHitbox)).Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		Texture2D glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(glow, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, glow.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.ai[0] != 1f)
		{
			return base.CanHitNPC(target);
		}
		return false;
	}

	public override bool CanHitPvp(Player target)
	{
		return base.Projectile.ai[0] != 1f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<EclipseStealthBoom>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack * 0.5f, base.Projectile.owner);
		}
	}
}
