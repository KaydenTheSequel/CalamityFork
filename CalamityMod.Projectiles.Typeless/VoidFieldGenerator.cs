using System;
using CalamityMod.CalPlayer;
using CalamityMod.Graphics.Metaballs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class VoidFieldGenerator : ModProjectile, ILocalizedModType, IModType
{
	public bool start = true;

	public StreamGougeMetaball.CosmicParticle VoidAura;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft *= 5;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!modPlayer.voidField)
		{
			base.Projectile.active = false;
			return;
		}
		if (player.dead)
		{
			modPlayer.voidField = false;
		}
		if (modPlayer.voidField)
		{
			base.Projectile.timeLeft = 2;
		}
		if (base.Projectile.ai[1] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.position);
		}
		Vector2 vector = player.Center - base.Projectile.Center;
		base.Projectile.Center = player.Center + Utils.RotatedBy(new Vector2(300f, 0f), (double)(base.Projectile.ai[1] + base.Projectile.ai[0] * ((float)Math.PI / 2f)), default(Vector2));
		base.Projectile.ai[1] += 0.01f;
		base.Projectile.velocity.X = ((vector.X > 0f) ? (-1E-06f) : 0f);
		for (int k = 0; k < Main.projectile.Length; k++)
		{
			Projectile proj = Main.projectile[k];
			if (proj.active && proj.owner == base.Projectile.owner && proj.arrow && !proj.Calamity().nihilicArrow && proj.friendly && Vector2.Distance(proj.Center, base.Projectile.Center) < 65f)
			{
				Main.projectile[k].damage = (int)((float)proj.damage * 1.75f);
				proj.extraUpdates++;
				Main.projectile[k].Calamity().nihilicArrow = true;
				SoundStyle style = SoundID.Item104 with
				{
					Volume = SoundID.Item104.Volume * 0.75f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int i = 0; i < 12; i++)
				{
					Vector2 dustpos = Vector2.UnitX * (0f - (float)proj.width) / 2f;
					dustpos += -Vector2.UnitY.RotatedBy((float)i * (float)Math.PI / 6f) * new Vector2(8f, 16f);
					dustpos = dustpos.RotatedBy(proj.rotation - (float)Math.PI / 2f);
					int dust = Dust.NewDust(proj.Center, 0, 0, 27, 0f, 0f, 100, Color.HotPink);
					Main.dust[dust].scale = 1.1f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = proj.Center + dustpos;
					Main.dust[dust].velocity = proj.velocity * 0.1f;
					Main.dust[dust].velocity = Vector2.Normalize(proj.Center - proj.velocity * 3f - Main.dust[dust].position) * 1.25f;
				}
			}
		}
		if (VoidAura == null)
		{
			VoidAura = VoidGeneratorMetaball.SpawnParticle(base.Projectile.Center, Vector2.Zero, 120f);
			return;
		}
		VoidAura.Center = base.Projectile.Center;
		VoidAura.Size = 120f;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}
}
