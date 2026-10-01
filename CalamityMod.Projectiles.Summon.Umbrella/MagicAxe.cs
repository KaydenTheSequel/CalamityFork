using System;
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

public class MagicAxe : ModProjectile, ILocalizedModType, IModType
{
	public float Behavior;

	private const float drawOffset = (float)Math.PI * 3f / 4f;

	public VertexStrip TrailDrawer;

	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float ChargeCooldown => ref base.Projectile.ai[1];

	public ref float TreeCounter => ref base.Projectile.localAI[0];

	public ref float TreeReset => ref base.Projectile.localAI[1];

	public float GetOffsetAngle => 3.7699113f + Main.projectile[(int)base.Projectile.ai[0]].ai[0] / 27f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 40;
		ProjectileID.Sets.TrailingMode[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 52);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
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
			CooldownReset(offense: false);
			IdleAI();
			return;
		}
		base.Projectile.rotation += 0.15f;
		if (CooldownReset(offense: true))
		{
			AttackEnemy(targetIndex);
		}
	}

	private bool CooldownReset(bool offense)
	{
		if (Behavior == 2f)
		{
			TreeReset = 0f;
			ChargeCooldown++;
			if (!(ChargeCooldown > 15f))
			{
				return false;
			}
			ChargeCooldown = 1f;
			Behavior = 0f;
			base.Projectile.netUpdate = true;
		}
		if (offense && Behavior == 4f)
		{
			Behavior = 0f;
		}
		return true;
	}

	private void AttackEnemy(int targetIndex)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		NPC npc = Main.npc[targetIndex];
		if (Behavior == 0f)
		{
			Vector2 targetSpot = npc.Center - base.Projectile.Center;
			float num = ((Vector2)(ref targetSpot)).Length();
			((Vector2)(ref targetSpot)).Normalize();
			if (num > 200f)
			{
				float speed = 48f;
				targetSpot *= speed;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + targetSpot) / 41f;
			}
			else
			{
				float speed2 = -24f;
				targetSpot *= speed2;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + targetSpot) / 41f;
			}
		}
		if (ChargeCooldown > 0f)
		{
			ChargeCooldown++;
		}
		if (ChargeCooldown > 15f)
		{
			ChargeCooldown = 0f;
			base.Projectile.netUpdate = true;
		}
		if (Behavior != 0f)
		{
			return;
		}
		Vector2 targetVec = npc.Center - base.Projectile.Center;
		float targetDist = ((Vector2)(ref targetVec)).Length();
		if (ChargeCooldown == 0f && targetDist < 500f)
		{
			ChargeCooldown++;
			if (Main.myPlayer == base.Projectile.owner)
			{
				Behavior = 2f;
				((Vector2)(ref targetVec)).Normalize();
				base.Projectile.velocity = targetVec * 30f;
				base.Projectile.netUpdate = true;
			}
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
			Behavior = 4f;
		}
		TreeCounter = 0f;
		TreeReset = 0f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	public Color TrailColorFunction(float completionRatio)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		float opacity = (float)Math.Pow(Utils.GetLerpValue(1f, 0.45f, completionRatio, clamped: true), 4.0) * base.Projectile.Opacity * 0.48f;
		return new Color(0, 255, 111) * opacity;
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
		if (Behavior != 4f)
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
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Vector2 dspeed = default(Vector2);
		for (int i = 0; i < 10; i++)
		{
			((Vector2)(ref dspeed))._002Ector(Main.rand.NextFloat(-7f, 7f), Main.rand.NextFloat(-7f, 7f));
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, 66, dspeed.X, dspeed.Y, 160, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 0.75f);
			Main.dust[dust].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffect(target.Center, target.whoAmI);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffect(target.Center, target.whoAmI);
	}

	private void OnHitEffect(Vector2 targetPos, int whoAmI)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (TreeReset == 0f)
		{
			TreeCounter++;
			TreeReset = 1f;
		}
		if (TreeCounter >= 20f)
		{
			TreeCounter = 0f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), targetPos + new Vector2(0f, -600f), Vector2.Zero, ModContent.ProjectileType<MagicTree>(), base.Projectile.damage * 10, base.Projectile.knockBack * 3f, base.Projectile.owner, whoAmI);
		}
	}
}
