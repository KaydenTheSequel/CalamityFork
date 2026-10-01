using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PwnagehammerProj : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/PwnagehammerSound")
	{
		Volume = 0.35f
	};

	public static readonly SoundStyle UseSoundFunny = new SoundStyle("CalamityMod/Sounds/Item/CalamityBell")
	{
		Volume = 1.5f
	};

	public static int HighBong = 0;

	public int time;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/Pwnagehammer";

	public ref int EmpoweredHammer => ref Main.player[base.Projectile.owner].Calamity().Holyhammer;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 7;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 66);
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 3600;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.direction = (base.Projectile.spriteDirection = ((base.Projectile.velocity.X > 0f) ? 1 : (-1)));
		base.Projectile.rotation += MathHelper.ToRadians(22.5f) * (float)base.Projectile.direction;
		if (EmpoweredHammer >= 5)
		{
			EmpoweredHammer = 0;
		}
		int falloffTime = 10;
		if (time > falloffTime)
		{
			base.Projectile.velocity.X *= 0.9711f;
		}
		if (base.Projectile.velocity.Y < 15f && time > falloffTime)
		{
			base.Projectile.velocity.Y += 0.426f;
		}
		if (base.Projectile.velocity.Y < 5f)
		{
			base.Projectile.velocity.Y *= 0.98f;
		}
		if (Main.rand.NextBool(3))
		{
			Vector2 offset = Utils.RotatedByRandom(new Vector2(12f, 0f), MathHelper.ToRadians(360f));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(4f, 0f), (double)offset.ToRotation(), default(Vector2));
			Dust.NewDustPerfect(new Vector2(base.Projectile.Center.X, base.Projectile.Center.Y) + offset, 228, (Vector2?)new Vector2(base.Projectile.velocity.X * 0.2f + velOffset.X, base.Projectile.velocity.Y * 0.2f + velOffset.Y), 100, new Color(255, 245, 198), 1.5f).noGravity = true;
		}
		time++;
	}

	public override bool PreKill(int timeLeft)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		float numberOfDusts = 13f;
		float rotFactor = 360f / numberOfDusts;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			float rot = MathHelper.ToRadians((float)i * rotFactor);
			Vector2 offset = Utils.RotatedBy(new Vector2(9f, 0f), (double)rot, default(Vector2));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(6f, 0f), (double)rot, default(Vector2));
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, 269, (Vector2?)new Vector2(velOffset.X, velOffset.Y), 0, default(Color), 1f);
			dust.noGravity = true;
			dust.velocity = velOffset;
			dust.scale = 2.5f;
		}
		if (Main.zenithWorld)
		{
			if (HighBong == 1)
			{
				SoundStyle style = UseSoundFunny with
				{
					Pitch = 0.2f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				HighBong = 0;
			}
			else
			{
				SoundStyle style = UseSoundFunny with
				{
					Pitch = (float)EmpoweredHammer * 0.1f - 0.2f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
		}
		else if (HighBong == 1)
		{
			SoundStyle style = UseSound with
			{
				Pitch = 0.2f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			HighBong = 0;
		}
		else
		{
			SoundStyle style = UseSound with
			{
				Pitch = (float)EmpoweredHammer * 0.1f - 0.2f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 0f, ModContent.ProjectileType<PwnagehammerExplosionSmall>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner);
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (EmpoweredHammer >= 3)
		{
			base.Projectile.ai[1] = target.whoAmI;
			int hammer = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, new Vector2(base.Projectile.velocity.SafeNormalize(Vector2.UnitX).X * 5f, -15f), ModContent.ProjectileType<PwnagehammerEcho>(), base.Projectile.damage * 2, base.Projectile.knockBack * 1.5f, base.Projectile.owner, 0f, base.Projectile.ai[1]);
			Main.projectile[hammer].localAI[0] = Math.Sign(base.Projectile.velocity.X);
			Main.projectile[hammer].netUpdate = true;
			EmpoweredHammer = 0;
			HighBong = 1;
		}
		else
		{
			EmpoweredHammer++;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
