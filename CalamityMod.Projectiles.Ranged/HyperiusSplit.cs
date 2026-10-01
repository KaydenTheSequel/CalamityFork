using System;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HyperiusSplit : ModProjectile, ILocalizedModType, IModType
{
	private Color currentColor;

	private int rotDirection;

	private float rotIntensity;

	private bool rotPhase2;

	public bool photosen;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.aiStyle = 1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 4;
		base.Projectile.timeLeft = 500;
		base.Projectile.extraUpdates = 4;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.alpha = 255;
		base.Projectile.ignoreWater = true;
		base.AIType = 14;
	}

	public override void AI()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0]++;
		int trailLifetime = 8;
		if (currentColor == Color.Black)
		{
			_ = Main.player[base.Projectile.owner];
			base.Projectile.scale = 0.015f;
			base.Projectile.alpha = 255;
			rotDirection = (Main.rand.NextBool() ? 1 : (-1));
			rotIntensity = Main.rand.NextFloat(0.3f, 1.1f);
			base.Projectile.timeLeft = Main.rand.Next(250, 301);
			float num = base.Projectile.ai[2];
			if (num != 4f)
			{
				if (num != 3f)
				{
					if (num != 2f)
					{
						if (num == 1f)
						{
							currentColor = Color.Cyan;
						}
						else
						{
							currentColor = Color.Lime;
						}
					}
					else
					{
						currentColor = Color.Red;
					}
				}
				else
				{
					currentColor = Color.Magenta;
				}
			}
			else
			{
				currentColor = Color.Yellow;
			}
		}
		if (base.Projectile.localAI[0] > 4f && base.Projectile.localAI[0] % 2f == 0f && !photosen)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 9f, -base.Projectile.velocity * 0.01f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, trailLifetime, 0.15f, currentColor, new Vector2(0.8f, 1.3f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.6f / rotIntensity, 0.8f, 0.7f));
		}
		if (base.Projectile.timeLeft == 180)
		{
			rotPhase2 = true;
		}
		if (rotPhase2)
		{
			rotIntensity *= 1.003f;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.995f;
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(-0.04f * rotIntensity * (float)rotDirection);
		}
		else
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.02f * rotIntensity * (float)rotDirection);
		}
	}

	public override void OnKill(int timeLeft)
	{
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSpark", (AssetRequestMode)2).Value;
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		Color val = currentColor;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(texture, position, null, val * (photosen ? 0.4f : 1f), base.Projectile.rotation, texture.Size() * 0.5f, new Vector2(0.9f, 1.5f) * base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		CalamityGlobalNPC modNPC = target.Calamity();
		if (!modNPC.hyperiusMarked)
		{
			modNPC.hyperiusMarked = true;
		}
		Player Owner = Main.player[base.Projectile.owner];
		bool crit = (float)Main.rand.Next(0, 101) < Owner.GetTotalCritChance(base.Projectile.DamageType);
		modNPC.hyperiusDamage += Math.Max(base.Projectile.damage * ((!crit) ? 1 : 2) - 1, 1);
		modifiers.DisableCrit();
		modifiers.SourceDamage *= 0f;
		modifiers.FinalDamage.Flat = 0.1f;
		modifiers.HideCombatText();
	}

	public HyperiusSplit()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		currentColor = Color.Black;
		rotDirection = 1;
		photosen = CalamityClientConfig.Instance.Photosensitivity;
		base._002Ector();
	}
}
