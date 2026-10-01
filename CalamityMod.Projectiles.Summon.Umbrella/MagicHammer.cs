using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.Umbrella;

public class MagicHammer : ModProjectile, ILocalizedModType, IModType
{
	public float Behavior;

	public float PivotPointX;

	public float PivotPointY;

	private int counter;

	private const int projSize = 60;

	private const float drawOffset = (float)Math.PI * 3f / 4f;

	public static readonly SoundStyle StylishSound = new SoundStyle("CalamityMod/Sounds/Custom/Stylish");

	public VertexStrip TrailDrawer;

	public new string LocalizationCategory => "Projectiles.Summon";

	public float GetOffsetAngle => (float)Math.PI * 4f / 5f + Main.projectile[(int)base.Projectile.ai[0]].ai[0] / 27f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 40;
		ProjectileID.Sets.TrailingMode[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.minion = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 40;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(Behavior);
		writer.Write(PivotPointX);
		writer.Write(PivotPointY);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Behavior = reader.ReadSingle();
		PivotPointX = reader.ReadSingle();
		PivotPointY = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.alpha -= 50;
		if (player.Calamity().magicHat)
		{
			base.Projectile.timeLeft = 2;
		}
		float homingRange = 1500.0001f;
		int targetIndex = -1;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float extraDist = npc.width / 2 + npc.height / 2;
				float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (targetIndex == -1 && targetDist < homingRange + extraDist)
				{
					homingRange = targetDist;
					targetIndex = npc.whoAmI;
				}
			}
		}
		if (targetIndex == -1)
		{
			for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
			{
				NPC npc2 = Main.npc[npcIndex];
				if (npc2.CanBeChasedBy(base.Projectile))
				{
					float extraDist2 = npc2.width / 2 + npc2.height / 2;
					float targetDist2 = Vector2.Distance(npc2.Center, base.Projectile.Center);
					if (targetDist2 < homingRange + extraDist2)
					{
						homingRange = targetDist2;
						targetIndex = npc2.whoAmI;
					}
				}
			}
		}
		if (targetIndex == -1)
		{
			Behavior = 0f;
			IdleAI();
		}
		else
		{
			MoveToEnemy(targetIndex);
		}
	}

	private void IdleAI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		Vector2 returnPos = Main.player[base.Projectile.owner].Center + GetOffsetAngle.ToRotationVector2() * 180f;
		Vector2 playerVec = returnPos - base.Projectile.Center;
		float num = ((Vector2)(ref playerVec)).Length();
		float playerHomeSpeed = 40f;
		if (num > 2000f)
		{
			base.Projectile.Center = returnPos;
			base.Projectile.netUpdate = true;
		}
		if (num > 60f)
		{
			((Vector2)(ref playerVec)).Normalize();
			playerVec *= playerHomeSpeed;
			base.Projectile.velocity = (base.Projectile.velocity * 10f + playerVec) / 11f;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI * 3f / 4f;
		}
		else
		{
			base.Projectile.Center = returnPos;
			base.Projectile.rotation = GetOffsetAngle + (float)Math.PI * 3f / 4f;
		}
		if (base.Projectile.scale != 1f)
		{
			base.Projectile.scale = 1f;
			base.Projectile.ExpandHitboxBy((int)(60f * base.Projectile.scale));
		}
	}

	private void MoveToEnemy(int targetIndex)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		if (Behavior == 0f)
		{
			Behavior = 1f;
		}
		Vector2 targetVec = Main.npc[targetIndex].Center - base.Projectile.Center;
		float num = ((Vector2)(ref targetVec)).Length();
		float moveSpeed = 40f;
		if (num > 60f && Behavior == 1f)
		{
			((Vector2)(ref targetVec)).Normalize();
			targetVec *= moveSpeed;
			base.Projectile.velocity = (base.Projectile.velocity * 1f + targetVec) / 2f;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI * 3f / 4f;
			return;
		}
		if (Behavior == 1f)
		{
			if (base.Projectile.scale == 1f)
			{
				base.Projectile.scale = 3f;
				base.Projectile.ExpandHitboxBy(base.Projectile.scale);
			}
			base.Projectile.rotation = (float)Math.PI * 3f / 4f + MathHelper.ToRadians(20f) * ((targetVec.X > 0f) ? 3f : (-3f));
			base.Projectile.ai[1] = (float)Math.PI * 3f / 4f + MathHelper.ToRadians(20f) * ((targetVec.X > 0f) ? 3f : (-3f));
			PivotPointX = base.Projectile.Bottom.X;
			PivotPointY = base.Projectile.Bottom.Y;
			Behavior = 2f;
		}
		if (Behavior == 2f)
		{
			Behavior = ((targetVec.X > 0f) ? 3f : 4f);
		}
		if (Behavior == 3f || Behavior == 4f)
		{
			float swingTime = 20f;
			base.Projectile.ai[1] += MathHelper.ToRadians(200f / swingTime) * ((Behavior == 3f) ? 1f : (-1f));
			counter++;
			float outwardPosition = 50f;
			Vector2 pivot = default(Vector2);
			((Vector2)(ref pivot))._002Ector(PivotPointX, PivotPointY);
			base.Projectile.Center = pivot + base.Projectile.ai[1].ToRotationVector2() * outwardPosition;
			base.Projectile.rotation = base.Projectile.ai[1] + (float)Math.PI * 3f / 4f;
			if ((float)counter > swingTime)
			{
				base.Projectile.ai[1] = (float)Math.PI / 4f;
				counter = 0;
				Behavior = 5f;
			}
		}
		if (Behavior == 5f)
		{
			MoveBackToPlayer();
		}
	}

	private void MoveBackToPlayer()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		Vector2 playerVec = Main.player[base.Projectile.owner].Center + GetOffsetAngle.ToRotationVector2() * 180f - base.Projectile.Center;
		float num = ((Vector2)(ref playerVec)).Length();
		float playerHomeSpeed = 40f;
		if (num > 60f)
		{
			((Vector2)(ref playerVec)).Normalize();
			playerVec *= playerHomeSpeed;
			base.Projectile.velocity = (base.Projectile.velocity * 1f + playerVec) / 2f;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI * 3f / 4f;
		}
		else
		{
			Behavior = 0f;
		}
		if (base.Projectile.scale != 1f)
		{
			base.Projectile.scale = 1f;
			base.Projectile.ExpandHitboxBy(60);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		if ((Behavior != 3f && Behavior != 4f) || !Main.rand.NextBool(20) || base.Projectile.soundDelay > 0)
		{
			return;
		}
		CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), new Color(239, 113, 152), CalamityUtils.GetTextValue("Misc.StylishHammerHit"), dramatic: true);
		SoundStyle style = StylishSound with
		{
			Volume = 0.35f
		};
		SoundEngine.PlaySound(in style, target.Center);
		base.Projectile.soundDelay = 60;
		for (int i = 0; i < 5; i++)
		{
			int confettiDust = Main.rand.Next(139, 143);
			int confetti = Dust.NewDust(target.Center, target.width, target.height, confettiDust, target.velocity.X, target.velocity.Y, 0, default(Color), 1.2f);
			Main.dust[confetti].velocity.X *= Main.rand.NextFloat(0.5f, 1.5f);
			Main.dust[confetti].velocity.Y *= Main.rand.NextFloat(0.5f, 1.5f);
			Main.dust[confetti].velocity.X += Main.rand.NextFloat(-2.5f, 2.5f);
			Main.dust[confetti].velocity.Y += Main.rand.NextFloat(-2.5f, 2.5f);
			Main.dust[confetti].scale *= Main.rand.NextFloat(0.7f, 1.3f);
			if (!Main.dedServ && Main.rand.NextBool())
			{
				int confettiGore = Main.rand.Next(276, 283);
				int idx = Gore.NewGore(base.Projectile.GetSource_FromThis(), target.Center, target.velocity, confettiGore);
				Main.gore[idx].velocity.X *= Main.rand.NextFloat(0.5f, 1.5f);
				Main.gore[idx].velocity.Y *= Main.rand.NextFloat(0.5f, 1.5f);
				Main.gore[idx].velocity.X += Main.rand.NextFloat(-2.5f, 2.5f);
				Main.gore[idx].velocity.Y += Main.rand.NextFloat(-2.5f, 2.5f);
				Main.gore[idx].scale *= Main.rand.NextFloat(0.8f, 1.2f);
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255, base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Vector2 dspeed = default(Vector2);
		for (int i = 0; i < 10; i++)
		{
			((Vector2)(ref dspeed))._002Ector(Main.rand.NextFloat(-7f, 7f), Main.rand.NextFloat(-7f, 7f));
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, 67, dspeed.X, dspeed.Y, 50, default(Color), 1.2f);
			Main.dust[dust].noGravity = true;
		}
	}

	public Color TrailColorFunction(float completionRatio)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		float opacity = (float)Math.Pow(Utils.GetLerpValue(1f, 0.45f, completionRatio, clamped: true), 4.0) * base.Projectile.Opacity * 0.48f;
		return new Color(255, 56, 0) * opacity;
	}

	public float TrailWidthFunction(float completionRatio)
	{
		return 2f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection == 1);
		if (Behavior != 0f)
		{
			if (TrailDrawer == null)
			{
				TrailDrawer = new VertexStrip();
			}
			GameShaders.Misc["EmpressBlade"].UseShaderSpecificData(new Vector4(1f, 0f, 0f, 0.6f));
			GameShaders.Misc["EmpressBlade"].Apply();
			TrailDrawer.PrepareStrip(base.Projectile.oldPos, base.Projectile.oldRot, TrailColorFunction, TrailWidthFunction, base.Projectile.Size * 0.5f - Main.screenPosition, base.Projectile.oldPos.Length, includeBacksides: true);
			TrailDrawer.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}
		Main.CurrentDrawnEntityShader = Main.player[base.Projectile.owner]?.cMinion ?? 0;
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction);
		return false;
	}
}
