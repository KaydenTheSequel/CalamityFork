using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class DrizzlefishFire : ModProjectile, ILocalizedModType, IModType
{
	private int splitTimer = 45;

	public int Time;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 90;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		if (Time < 7)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value);
		}
		else if (base.Projectile.ai[1] == 1f)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/DrizzlefishFire2", (AssetRequestMode)2).Value);
		}
		else
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/DrizzlefishFire", (AssetRequestMode)2).Value);
		}
		if (base.Projectile.ai[1] == 1f)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/DrizzlefishFire2", (AssetRequestMode)2).Value;
			Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, 16, 16), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, 10f), base.Projectile.scale, (SpriteEffects)0, 0f);
			return false;
		}
		return true;
	}

	public override void AI()
	{
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		Player Owner = Main.player[base.Projectile.owner];
		if (Main.zenithWorld && Time == 1 && Owner.Calamity().dragoonDrizzlefishGelBoost > 1)
		{
			base.Projectile.damage = base.Projectile.damage * Owner.Calamity().dragoonDrizzlefishGelBoost;
		}
		base.Projectile.scale = 1.5f;
		int dustType = 235;
		int dustType2 = 235;
		dustType = ((base.Projectile.ai[1] == 1f) ? ((!Main.rand.NextBool()) ? 162 : 174) : ((!Main.rand.NextBool()) ? 90 : 183));
		if (Time > 7)
		{
			base.Projectile.alpha = 0;
			for (int i = 0; i < 5; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(9f, 9f) - base.Projectile.velocity * 1.5f, dustType, -base.Projectile.velocity);
				dust.noGravity = true;
				dust.velocity *= 0f;
				dust.scale = ((Owner.Calamity().dragoonDrizzlefishGelBoost > 1) ? Main.rand.NextFloat(0.7f + (float)Owner.Calamity().dragoonDrizzlefishGelBoost * 0.5f, 1.4f + (float)Owner.Calamity().dragoonDrizzlefishGelBoost * 0.5f) : Main.rand.NextFloat(1.2f, 1.9f));
			}
		}
		else
		{
			base.Projectile.alpha = 255;
		}
		if (Time == 4)
		{
			for (int j = 0; j <= 16; j++)
			{
				Dust obj = Dust.NewDustPerfect(Type: (base.Projectile.ai[1] != 1f) ? ((!Main.rand.NextBool()) ? 90 : 183) : ((!Main.rand.NextBool()) ? 162 : 174), Position: base.Projectile.Center, Velocity: base.Projectile.velocity);
				obj.scale = Main.rand.NextFloat(1.8f, 2.3f);
				obj.velocity = base.Projectile.velocity.RotatedByRandom(1.100000023841858) * Main.rand.NextFloat(0.6f, 1.9f);
				obj.noGravity = true;
			}
		}
		splitTimer--;
		if (splitTimer <= 0)
		{
			int numProj = 2;
			float rotation = MathHelper.ToRadians((float)Main.rand.Next(15, 26));
			if (base.Projectile.owner == Main.myPlayer)
			{
				if (base.Projectile.ai[1] == 1f)
				{
					for (int k = 0; k < numProj + 1; k++)
					{
						Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(base.Projectile.velocity.X, base.Projectile.velocity.Y), (double)MathHelper.Lerp(0f - rotation, rotation, (float)(k / (numProj - 1))), default(Vector2));
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<DrizzlefishFireSplit>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 1f);
					}
				}
				else
				{
					for (int l = 0; l < numProj + 1; l++)
					{
						Vector2 perturbedSpeed2 = Utils.RotatedBy(new Vector2(base.Projectile.velocity.X, base.Projectile.velocity.Y), (double)MathHelper.Lerp(0f - rotation, rotation, (float)(l / (numProj - 1))), default(Vector2));
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, perturbedSpeed2.X, perturbedSpeed2.Y, ModContent.ProjectileType<DrizzlefishFireSplit>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
					}
				}
			}
			base.Projectile.Kill();
		}
		Lighting.AddLight(base.Projectile.Center, 0.25f, 0f, 0f);
		if (base.Projectile.timeLeft > 90)
		{
			base.Projectile.timeLeft = 90;
		}
		base.Projectile.rotation += 0.5f * (float)base.Projectile.direction;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.ai[1] == 1f)
		{
			target.AddBuff(323, 120);
		}
		else
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 60);
		}
	}
}
