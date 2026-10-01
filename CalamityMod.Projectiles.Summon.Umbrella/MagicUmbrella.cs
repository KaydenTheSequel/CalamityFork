using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.Umbrella;

public class MagicUmbrella : ModProjectile, ILocalizedModType, IModType
{
	public float Behavior;

	private const float drawOffset = (float)Math.PI * 3f / 4f;

	public VertexStrip TrailDrawer;

	public new string LocalizationCategory => "Projectiles.Summon";

	public float GetOffsetAngle => 5.0265484f + Main.projectile[(int)base.Projectile.ai[0]].ai[0] / 27f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 40;
		ProjectileID.Sets.TrailingMode[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 14);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
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
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.alpha -= 50;
		if (Main.player[base.Projectile.owner].Calamity().magicHat)
		{
			base.Projectile.timeLeft = 2;
		}
		List<int> blackListedTargets = new List<int>();
		Color transparent = Color.Transparent;
		DelegateMethods.v3_1 = ((Color)(ref transparent)).ToVector3();
		Point point = base.Projectile.Center.ToTileCoordinates();
		DelegateMethods.CastLightOpen(point.X, point.Y);
		blackListedTargets.Clear();
		DecideWhatToDo(blackListedTargets);
	}

	private void DecideWhatToDo(List<int> blacklist)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		int num = 40;
		int num2 = num - 1;
		int num3 = num + 40;
		int num4 = num3 - 1;
		int num5 = num + 1;
		Player player = Main.player[base.Projectile.owner];
		if (player.active && Vector2.Distance(player.Center, base.Projectile.Center) > 2000f)
		{
			Behavior = 0f;
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (Behavior == -1f)
		{
			IdleAI();
			return;
		}
		if (Behavior == 0f)
		{
			IdleAI();
			int targetIdx = FindATarget(blacklist);
			if (targetIdx != -1)
			{
				Behavior = Main.rand.NextFromList<int>(num, num3);
				Behavior = num3;
				base.Projectile.ai[1] = targetIdx;
				base.Projectile.netUpdate = true;
			}
			return;
		}
		int num13 = 0;
		int num14 = num2;
		int num15 = 0;
		if (Behavior >= (float)num5)
		{
			num13 = 1;
			num14 = num4;
			num15 = num5;
		}
		int currentTarget = (int)base.Projectile.ai[1];
		if (!Main.npc.IndexInRange(currentTarget))
		{
			int targetIdx2 = FindATarget(blacklist);
			if (targetIdx2 != -1)
			{
				Behavior = Main.rand.NextFromList<int>(num, num3);
				base.Projectile.ai[1] = targetIdx2;
				base.Projectile.netUpdate = true;
			}
			else
			{
				Behavior = -1f;
				base.Projectile.ai[1] = 0f;
				base.Projectile.netUpdate = true;
			}
			return;
		}
		NPC npc = Main.npc[currentTarget];
		if (!npc.CanBeChasedBy(this))
		{
			int targetIdx3 = FindATarget(blacklist);
			if (targetIdx3 != -1)
			{
				Behavior = Main.rand.NextFromList<int>(num, num3);
				base.Projectile.ai[1] = targetIdx3;
				base.Projectile.netUpdate = true;
			}
			else
			{
				Behavior = -1f;
				base.Projectile.ai[1] = 0f;
				base.Projectile.netUpdate = true;
			}
			return;
		}
		Behavior--;
		if (Behavior >= (float)num14)
		{
			base.Projectile.direction = ((base.Projectile.Center.X < npc.Center.X) ? 1 : (-1));
			if (Behavior == (float)num14)
			{
				base.Projectile.localAI[0] = base.Projectile.Center.X;
				base.Projectile.localAI[1] = base.Projectile.Center.Y;
			}
		}
		float lerpValue2 = Utils.GetLerpValue(num14, num15, Behavior, clamped: true);
		if (num13 == 0)
		{
			Vector2 vector6 = default(Vector2);
			((Vector2)(ref vector6))._002Ector(base.Projectile.localAI[0], base.Projectile.localAI[1]);
			if (lerpValue2 >= 0.5f)
			{
				vector6 = Vector2.Lerp(npc.Center, Main.player[base.Projectile.owner].Center, 0.5f);
			}
			Vector2 center2 = npc.Center;
			float num19 = (center2 - vector6).ToRotation();
			float num20 = ((base.Projectile.direction == 1) ? (-(float)Math.PI) : ((float)Math.PI));
			float num21 = num20 + (0f - num20) * lerpValue2 * 2f;
			Vector2 vector7 = num21.ToRotationVector2();
			vector7.Y *= 0.5f;
			vector7.Y *= 0.8f + (float)Math.Sin((float)base.Projectile.identity * 2.3f) * 0.2f;
			Vector2 spinningpoint = vector7;
			double radians = num19;
			Vector2 center3 = default(Vector2);
			vector7 = spinningpoint.RotatedBy(radians, center3);
			center3 = center2 - vector6;
			float scaleFactor2 = ((Vector2)(ref center3)).Length() / 2f;
			center3 = (base.Projectile.Center = Vector2.Lerp(vector6, center2, 0.5f) + vector7 * scaleFactor2);
			float num22 = MathHelper.WrapAngle(num19 + num21);
			base.Projectile.rotation = num22 + (float)Math.PI * 3f / 4f;
			base.Projectile.velocity = num22.ToRotationVector2() * 10f;
			Projectile projectile = base.Projectile;
			projectile.position -= base.Projectile.velocity;
		}
		if (num13 == 1)
		{
			Vector2 vector11 = default(Vector2);
			((Vector2)(ref vector11))._002Ector(base.Projectile.localAI[0], base.Projectile.localAI[1]);
			vector11 += new Vector2(0f, Utils.GetLerpValue(0f, 0.4f, lerpValue2, clamped: true) * -100f);
			Vector2 v = npc.Center - vector11;
			Vector2 value = v.SafeNormalize(Vector2.Zero) * MathHelper.Clamp(((Vector2)(ref v)).Length(), 60f, 150f);
			Vector2 value2 = npc.Center + value;
			float lerpValue3 = Utils.GetLerpValue(0.4f, 0.6f, lerpValue2, clamped: true);
			float lerpValue4 = Utils.GetLerpValue(0.6f, 1f, lerpValue2, clamped: true);
			float targetAngle = v.SafeNormalize(Vector2.Zero).ToRotation() + (float)Math.PI * 3f / 4f;
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(targetAngle, (float)Math.PI * 19f / 20f);
			base.Projectile.Center = Vector2.Lerp(vector11, npc.Center, lerpValue3);
			if (lerpValue4 > 0f)
			{
				base.Projectile.Center = Vector2.Lerp(npc.Center, value2, lerpValue4);
			}
		}
		if (Behavior == (float)num15)
		{
			int targetIdx4 = FindATarget(blacklist);
			if (targetIdx4 != -1)
			{
				Behavior = Main.rand.NextFromList<int>(num, num3);
				base.Projectile.ai[1] = targetIdx4;
				base.Projectile.netUpdate = true;
			}
			else
			{
				Behavior = -1f;
				base.Projectile.ai[1] = 0f;
				base.Projectile.netUpdate = true;
			}
		}
	}

	private int FindATarget(List<int> blackListedTargets)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = Main.player[base.Projectile.owner].Center;
		int target = -1;
		float closestDist = -1f;
		NPC selectedTarget = base.Projectile.OwnerMinionAttackTargetNPC;
		if (selectedTarget != null && selectedTarget.CanBeChasedBy(this))
		{
			bool flag = true;
			if (!selectedTarget.boss && blackListedTargets.Contains(selectedTarget.whoAmI))
			{
				flag = false;
			}
			if (selectedTarget.Distance(center) > 1500.0001f)
			{
				flag = false;
			}
			if (flag)
			{
				return selectedTarget.whoAmI;
			}
		}
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (npc.CanBeChasedBy(this) && (npc.boss || !blackListedTargets.Contains(npc.whoAmI)))
			{
				float npcDist = npc.Distance(center);
				if (npcDist <= 1500.0001f && (npcDist <= closestDist || closestDist == -1f))
				{
					closestDist = npcDist;
					target = npc.whoAmI;
				}
			}
		}
		return target;
	}

	private void IdleAI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 returnPos = Main.player[base.Projectile.owner].Center + GetOffsetAngle.ToRotationVector2() * 180f;
		bool returningToPlayer = Behavior == -1f;
		Vector2 playerVec = returnPos - base.Projectile.Center;
		float num = ((Vector2)(ref playerVec)).Length();
		float playerHomeSpeed = 40f;
		if (((num < 150f) & returningToPlayer) && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			Behavior = 0f;
			base.Projectile.netUpdate = true;
		}
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
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Vector2 dspeed = default(Vector2);
		for (int i = 0; i < 10; i++)
		{
			((Vector2)(ref dspeed))._002Ector(Main.rand.NextFloat(-7f, 7f), Main.rand.NextFloat(-7f, 7f));
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, 66, dspeed.X, dspeed.Y, 160, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 0.75f);
			Main.dust[dust].noGravity = true;
		}
	}

	public Color TrailColorFunction(float completionRatio)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		float opacity = (float)Math.Pow(Utils.GetLerpValue(1f, 0.45f, completionRatio, clamped: true), 4.0) * base.Projectile.Opacity * 0.48f;
		return new Color(75, 255, 255) * opacity;
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
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection == 1);
		if (Behavior > 0f)
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
