using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.Umbrella;

public class MagicArrow : ModProjectile, ILocalizedModType, IModType
{
	public float Behavior;

	private const float drawOffset = (float)Math.PI * 3f / 4f;

	public VertexStrip TrailDrawer;

	public new string LocalizationCategory => "Projectiles.Summon";

	public float GetOffsetAngle => (float)Math.PI * 2f / 5f + Main.projectile[(int)base.Projectile.ai[0]].ai[0] / 27f;

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(Behavior);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Behavior = reader.ReadSingle();
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
			IdleAI();
		}
		else
		{
			AttackEnemy(targetIndex);
		}
	}

	private void AttackEnemy(int targetIndex)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		NPC npc = Main.npc[targetIndex];
		Behavior = 0f;
		base.Projectile.velocity = (base.Projectile.velocity * 5f + base.Projectile.SafeDirectionTo(npc.Center) * 40f) / 6f;
		base.Projectile.rotation = base.Projectile.AngleTo(npc.Center) + (float)Math.PI * 3f / 4f;
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
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
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
			Behavior = 1f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	public Color TrailColorFunction(float completionRatio)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		float opacity = (float)Math.Pow(Utils.GetLerpValue(1f, 0.45f, completionRatio, clamped: true), 4.0) * base.Projectile.Opacity * 0.48f;
		return new Color(211, 8, 8) * opacity;
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
		if (Behavior != 1f)
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

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		OnHitEffect();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		OnHitEffect();
	}

	private void OnHitEffect()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		if (Behavior != 1f)
		{
			Vector2 dspeed = default(Vector2);
			for (int i = 0; i < 10; i++)
			{
				((Vector2)(ref dspeed))._002Ector(Main.rand.NextFloat(-7f, 7f), Main.rand.NextFloat(-7f, 7f));
				int dust = Dust.NewDust(base.Projectile.Center, 1, 1, 67, dspeed.X, dspeed.Y, 50, default(Color), 1.2f);
				Main.dust[dust].noGravity = true;
			}
			Vector2 returnPos = Main.player[base.Projectile.owner].Center + GetOffsetAngle.ToRotationVector2() * 180f;
			base.Projectile.Center = returnPos;
			Vector2 dspeed2 = default(Vector2);
			for (int j = 0; j < 10; j++)
			{
				((Vector2)(ref dspeed2))._002Ector(Main.rand.NextFloat(-7f, 7f), Main.rand.NextFloat(-7f, 7f));
				int dust2 = Dust.NewDust(base.Projectile.Center, 1, 1, 67, dspeed2.X, dspeed2.Y, 50, default(Color), 1.2f);
				Main.dust[dust2].noGravity = true;
			}
		}
	}
}
