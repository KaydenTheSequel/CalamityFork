using System;
using System.IO;
using CalamityMod.NPCs.Yharon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class YharonBulletHellVortex : ModProjectile, ILocalizedModType, IModType
{
	public int victim;

	public new string LocalizationCategory => "Projectiles.Boss";

	public ref float TimeCountdown => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 408;
		base.Projectile.height = 408;
		base.Projectile.scale = 0.05f;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 60000;
		base.Projectile.Calamity().DealsDefenseDamage = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(victim);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		victim = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		if ((Main.npc[(int)base.Projectile.ai[1]].active && Main.npc[(int)base.Projectile.ai[1]].type == ModContent.NPCType<Yharon>()) || Main.zenithWorld)
		{
			if (TimeCountdown > 0f)
			{
				if (TimeCountdown <= 20f)
				{
					base.Projectile.scale = MathHelper.Clamp(base.Projectile.scale - 0.05f, 0f, 1f);
				}
				else if (base.Projectile.scale < 1f)
				{
					base.Projectile.scale = MathHelper.Clamp(base.Projectile.scale + 0.05f, 0f, 1f);
				}
				TimeCountdown--;
				if (!Main.zenithWorld)
				{
					return;
				}
				base.Projectile.hostile = true;
				base.Projectile.width = (base.Projectile.height = (int)(408f * base.Projectile.scale));
				float inertia = 5f;
				float speed = 5.35f;
				float minDist = 160f;
				if (victim >= 0 && Main.player[victim].active && !Main.player[victim].dead)
				{
					if (base.Projectile.Distance(Main.player[victim].Center) > minDist)
					{
						Vector2 moveDirection = base.Projectile.SafeDirectionTo(Main.player[victim].Center, Vector2.UnitY);
						base.Projectile.velocity = (base.Projectile.velocity * (inertia - 1f) + moveDirection * speed) / inertia;
					}
				}
				else
				{
					victim = Player.FindClosest(base.Projectile.Center, 1, 1);
					base.Projectile.netUpdate = true;
				}
				float pushForce = 0.05f;
				for (int k = 0; k < Main.maxProjectiles; k++)
				{
					Projectile otherProj = Main.projectile[k];
					if (!otherProj.active || k == base.Projectile.whoAmI)
					{
						continue;
					}
					bool num = otherProj.type == base.Projectile.type;
					float taxicabDist = Vector2.Distance(base.Projectile.Center, otherProj.Center);
					float distancegate = 360f;
					if (num && taxicabDist < distancegate)
					{
						if (base.Projectile.position.X < otherProj.position.X)
						{
							base.Projectile.velocity.X -= pushForce;
						}
						else
						{
							base.Projectile.velocity.X += pushForce;
						}
						if (base.Projectile.position.Y < otherProj.position.Y)
						{
							base.Projectile.velocity.Y -= pushForce;
						}
						else
						{
							base.Projectile.velocity.Y += pushForce;
						}
					}
				}
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		else
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion();
		Texture2D vortexNoise = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Cracks", (AssetRequestMode)2).Value;
		GameShaders.Misc["CalamityMod:DoGPortal"].UseOpacity(1f);
		GameShaders.Misc["CalamityMod:DoGPortal"].UseColor(Color.Gold);
		GameShaders.Misc["CalamityMod:DoGPortal"].UseSecondaryColor(Color.White);
		GameShaders.Misc["CalamityMod:DoGPortal"].Apply();
		for (int i = 0; i < 10; i++)
		{
			float angle = (float)Math.PI * 2f * (float)i / 10f + Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f);
			Color drawColor = Color.White;
			((Color)(ref drawColor)).A = 0;
			Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + angle.ToRotationVector2() * 3f;
			Main.EntitySpriteDraw(vortexNoise, drawPosition, null, drawColor, angle + (float)Math.PI / 2f, vortexNoise.Size() * 0.5f, base.Projectile.scale * 1.7f, (SpriteEffects)0);
		}
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
