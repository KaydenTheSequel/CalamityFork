using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class AuroradicalSplitter : ModProjectile, ILocalizedModType, IModType
{
	public int[] dustTypes = new int[2]
	{
		ModContent.DustType<AstralBlue>(),
		ModContent.DustType<AstralOrange>()
	};

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Rogue/AuroradicalStar";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 100;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 50;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override void AI()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		Lighting.AddLight(base.Projectile.Center, 0.3f, 0.5f, 0.1f);
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 20 + Main.rand.Next(40);
			if (Main.rand.NextBool(5))
			{
				SoundEngine.PlaySound(in SoundID.Item9, base.Projectile.position);
			}
		}
		float scaleAmt = (float)(int)Main.mouseTextColor / 200f - 0.35f;
		scaleAmt *= 0.2f;
		base.Projectile.scale = scaleAmt + 0.95f;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 15f)
		{
			int astral = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.rand.Next(dustTypes), 0f, 0f, 100, default(Color), 0.8f);
			Main.dust[astral].noGravity = true;
			Dust obj = Main.dust[astral];
			obj.velocity *= 0f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.Item9, base.Projectile.position);
		for (float i = 0f; i < 5f; i++)
		{
			float angle = (float)Math.PI * 2f / 5f * i;
			int star = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, angle.ToRotationVector2() * 5f, ModContent.ProjectileType<AuroradicalStar>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			Main.projectile[star].Calamity().stealthStrike = base.Projectile.Calamity().stealthStrike;
		}
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 96);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int d = 0; d < 2; d++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.rand.Next(dustTypes), 0f, 0f, 50);
		}
		for (int j = 0; j < 20; j++)
		{
			int astral = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.rand.Next(dustTypes), 0f, 0f, 0, default(Color), 1.5f);
			Main.dust[astral].noGravity = true;
			Dust obj = Main.dust[astral];
			obj.velocity *= 3f;
			astral = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 50);
			Dust obj2 = Main.dust[astral];
			obj2.velocity *= 2f;
			Main.dust[astral].noGravity = true;
		}
		if (!Main.dedServ)
		{
			for (int g = 0; g < 3; g++)
			{
				Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position, new Vector2(base.Projectile.velocity.X * 0.05f, base.Projectile.velocity.Y * 0.05f), Main.rand.Next(16, 18));
			}
		}
	}
}
