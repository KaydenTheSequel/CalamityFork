using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class FallenPaladinsHammerEcho : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle SlamHamSound = new SoundStyle("CalamityMod/Sounds/Item/FallenPaladinsHammerBigImpact")
	{
		Volume = 0.6f
	};

	public static readonly SoundStyle Kunk = new SoundStyle("CalamityMod/Sounds/Item/TF2PanHit")
	{
		Volume = 1.1f
	};

	public float speed;

	public NPC targeted;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 62;
		base.Projectile.height = 62;
		base.Projectile.aiStyle = 0;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] < 42f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.95f;
			base.Projectile.rotation += 1f * Utils.GetLerpValue(30f, 0f, base.Projectile.ai[0]) * (float)base.Projectile.direction;
		}
		else if (base.Projectile.ai[0] >= 42f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
			base.Projectile.extraUpdates = 4;
			if (base.Projectile.ai[1] != -5f)
			{
				targeted = Main.npc[(int)base.Projectile.ai[1]];
			}
			if (targeted == null || !targeted.CanBeChasedBy(base.Projectile) || !targeted.active)
			{
				base.Projectile.ai[1] = -5f;
				targeted = base.Projectile.Center.ClosestNPCAt(2000f);
			}
			if (targeted != null)
			{
				CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.7f, 25f, 0.98f);
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		if (base.Projectile.ai[0] == 42f && targeted != null)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 278, ((targeted.Center - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 15f).RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.2f, 1f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.5f, 1.2f);
				dust.color = (Main.rand.NextBool() ? Color.IndianRed : Color.Red);
			}
		}
		if (Main.rand.NextBool())
		{
			Vector2 offset = Utils.RotatedByRandom(new Vector2(7f, 0f), MathHelper.ToRadians(360f));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(3f, 0f), (double)offset.ToRotation(), default(Vector2));
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + offset, 267, (Vector2?)new Vector2(base.Projectile.velocity.X * 0.2f + velOffset.X, base.Projectile.velocity.Y * 0.2f + velOffset.Y), 0, new Color(255, 245, 198), 1f);
			dust2.noGravity = true;
			dust2.color = Color.DarkRed;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (targeted != null && target != targeted)
		{
			modifiers.SourceDamage *= 0.3f;
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.ai[0] <= 42f)
		{
			return false;
		}
		return null;
	}

	public override bool CanHitPvp(Player target)
	{
		return base.Projectile.ai[0] > 42f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
		if (target == targeted)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreKill(int timeLeft)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		if (Main.zenithWorld)
		{
			SoundEngine.PlaySound(in Kunk, base.Projectile.Center);
		}
		else
		{
			SoundEngine.PlaySound(in SlamHamSound, base.Projectile.Center);
		}
		Main.player[base.Projectile.owner].SetScreenshake(5f);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 0.001f, ModContent.ProjectileType<FallenBlast>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner);
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/FallenPaladinsHammer", (AssetRequestMode)2).Value;
		Projectile projectile = base.Projectile;
		int mode = ProjectileID.Sets.TrailingMode[base.Type];
		Color val = Color.DarkRed;
		((Color)(ref val)).A = 0;
		CalamityUtils.DrawAfterimagesCentered(projectile, mode, val * 0.5f, 1, texture, drawCentered: true, shrink: true);
		Projectile projectile2 = base.Projectile;
		val = Color.Red;
		((Color)(ref val)).A = 0;
		projectile2.DrawProjectileWithBackglow(val, Color.Lerp(Color.Red, Color.White, 0.5f), 5f, texture, null, (SpriteEffects)0);
		return false;
	}
}
