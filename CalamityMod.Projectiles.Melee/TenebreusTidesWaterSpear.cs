using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TenebreusTidesWaterSpear : ModProjectile, ILocalizedModType, IModType
{
	private int penetrationAmt = 2;

	private bool dontDraw;

	private int drawInt;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Magic/UndinesRetributionSpear";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = penetrationAmt;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5 * base.Projectile.MaxUpdates;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(penetrationAmt);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		penetrationAmt = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.localAI[0]++;
			if (base.Projectile.localAI[0] > 7f)
			{
				int water = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 0.4f);
				Main.dust[water].noGravity = true;
				Dust obj = Main.dust[water];
				obj.velocity *= 0.5f;
				Dust obj2 = Main.dust[water];
				obj2.velocity += base.Projectile.velocity * 0.1f;
			}
			float scalar = 0.01f;
			int alphaAmt = 5;
			int alphaCeiling = alphaAmt * 15;
			int alphaFloor = 0;
			if (base.Projectile.localAI[0] > 7f)
			{
				if (base.Projectile.localAI[1] == 0f)
				{
					base.Projectile.scale -= scalar;
					base.Projectile.alpha += alphaAmt;
					if (base.Projectile.alpha > alphaCeiling)
					{
						base.Projectile.alpha = alphaCeiling;
						base.Projectile.localAI[1] = 1f;
					}
				}
				else if (base.Projectile.localAI[1] == 1f)
				{
					base.Projectile.scale += scalar;
					base.Projectile.alpha -= alphaAmt;
					if (base.Projectile.alpha <= alphaFloor)
					{
						base.Projectile.alpha = alphaFloor;
						base.Projectile.localAI[1] = 0f;
					}
				}
			}
		}
		else if (base.Projectile.ai[0] >= 1f && base.Projectile.ai[0] < (float)(1 + penetrationAmt))
		{
			base.Projectile.alpha += 15;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.98f;
			base.Projectile.localAI[0] = 0f;
			if (base.Projectile.alpha >= 255)
			{
				if (base.Projectile.ai[0] == 1f)
				{
					base.Projectile.Kill();
					return;
				}
				int whoAmI = -1;
				Vector2 targetSpot = base.Projectile.Center;
				float detectRange = 700f;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC npc = enumerator.Current;
					if (npc.CanBeChasedBy(base.Projectile))
					{
						float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
						if (targetDist < detectRange)
						{
							detectRange = targetDist;
							targetSpot = npc.Center;
							whoAmI = npc.whoAmI;
						}
					}
				}
				if (whoAmI >= 0)
				{
					base.Projectile.netUpdate = true;
					base.Projectile.ai[0] += penetrationAmt;
					base.Projectile.position = targetSpot + ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * 100f - new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f;
					dontDraw = true;
					base.Projectile.velocity = Vector2.Normalize(targetSpot - base.Projectile.Center) * 18f;
				}
				else
				{
					base.Projectile.Kill();
				}
			}
			if (Main.rand.NextBool(3))
			{
				int water2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 0.4f);
				Main.dust[water2].noGravity = true;
				Dust obj3 = Main.dust[water2];
				obj3.velocity *= 0.5f;
				Dust obj4 = Main.dust[water2];
				obj4.velocity += base.Projectile.velocity * 0.1f;
			}
		}
		else if (base.Projectile.ai[0] >= (float)(1 + penetrationAmt) && base.Projectile.ai[0] < (float)(1 + penetrationAmt * 2))
		{
			base.Projectile.scale = 0.9f;
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] >= 15f)
			{
				base.Projectile.alpha += 51;
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.8f;
				if (base.Projectile.alpha >= 255)
				{
					base.Projectile.Kill();
				}
			}
			else
			{
				base.Projectile.alpha -= 125;
				if (base.Projectile.alpha < 0)
				{
					base.Projectile.alpha = 0;
				}
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 0.98f;
			}
			base.Projectile.localAI[0]++;
			int water3 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 0.4f);
			Main.dust[water3].noGravity = true;
			Dust obj5 = Main.dust[water3];
			obj5.velocity *= 0.5f;
			Dust obj6 = Main.dust[water3];
			obj6.velocity += base.Projectile.velocity * 0.1f;
		}
		Lighting.AddLight((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16, 0f, 0f, (float)(255 - base.Projectile.alpha) * 1f / 255f);
		if (dontDraw)
		{
			drawInt++;
		}
		if (drawInt > 1)
		{
			drawInt = 0;
			dontDraw = false;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(50, 50, 255, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		if (dontDraw)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int frameHeight = height * base.Projectile.frame;
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, frameHeight, texture.Width, height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)height / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool? CanDamage()
	{
		if ((((int)(base.Projectile.ai[0] - 1f) / penetrationAmt == 0 && penetrationAmt < 3) || base.Projectile.ai[1] < 5f) && base.Projectile.ai[0] != 0f)
		{
			return false;
		}
		return true;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.ai[0] >= (float)(1 + penetrationAmt) && base.Projectile.ai[0] < (float)(1 + penetrationAmt * 2))
		{
			base.Projectile.ai[0] = 0f;
		}
		penetrationAmt--;
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.ai[0] += penetrationAmt;
		}
		else
		{
			base.Projectile.ai[0] -= penetrationAmt + 1;
		}
		base.Projectile.ai[1] = 0f;
		base.Projectile.netUpdate = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 120);
	}
}
