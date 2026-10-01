using System;
using CalamityMod.Sounds;
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

public class MagicRifle : ModProjectile, ILocalizedModType, IModType
{
	public VertexStrip TrailDrawer;

	public bool drawTrail;

	public bool leftSide;

	public int swapCooldown;

	public float sineCounter;

	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float SwapSides => ref base.Projectile.localAI[0];

	public ref float SpinCounter => ref base.Projectile.localAI[1];

	public ref float ShootCooldown => ref base.Projectile.ai[1];

	public float GetOffsetAngle => (float)Math.PI * 2f + Main.projectile[(int)base.Projectile.ai[0]].ai[0] / 27f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 40;
		ProjectileID.Sets.TrailingMode[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 50);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (player.Calamity().magicHat)
		{
			base.Projectile.timeLeft = 2;
		}
		if (swapCooldown > 0)
		{
			swapCooldown--;
		}
		if (swapCooldown == 1)
		{
			leftSide = !leftSide;
		}
		sineCounter++;
		float homingRange = 1500.0001f;
		Vector2 targetVec = base.Projectile.position;
		int targetIndex = -1;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float extraDist = npc.width / 2 + npc.height / 2;
				float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (targetDist < homingRange + extraDist)
				{
					homingRange = targetDist;
					targetVec = npc.Center;
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
						targetVec = npc2.Center;
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
			AttackMovement(targetIndex);
		}
		if ((float)swapCooldown != 0f)
		{
			return;
		}
		if (ShootCooldown > 0f)
		{
			ShootCooldown++;
		}
		if (ShootCooldown > 45f)
		{
			ShootCooldown = 0f;
			base.Projectile.netUpdate = true;
		}
		if (ShootCooldown != 0f || targetIndex == -1)
		{
			return;
		}
		if (SwapSides > 5f)
		{
			SpinCounter += MathHelper.ToRadians(60f) * (float)base.Projectile.spriteDirection;
			if (Math.Abs(SpinCounter) > MathHelper.ToRadians(720f))
			{
				SpinCounter = 0f;
				SwapSides = -1f;
			}
		}
		else
		{
			if (Main.myPlayer != base.Projectile.owner)
			{
				return;
			}
			float projSpeed = 6f;
			int projType = ModContent.ProjectileType<MagicBullet>();
			int damage = base.Projectile.damage;
			float kback = 0f;
			if (SwapSides == -1f)
			{
				projType = ModContent.ProjectileType<MagicBulletBig>();
				damage *= 2;
				swapCooldown = 30;
				kback = base.Projectile.knockBack;
			}
			ShootCooldown++;
			if (Main.myPlayer == base.Projectile.owner)
			{
				Vector2 velocity = targetVec - base.Projectile.Center;
				((Vector2)(ref velocity)).Normalize();
				velocity *= projSpeed;
				SoundEngine.PlaySound((SwapSides == -1f) ? CommonCalamitySounds.LargeWeaponFireSound : SoundID.Item40, base.Projectile.position);
				int bullet = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, projType, damage, kback, base.Projectile.owner);
				if (Main.projectile.IndexInRange(bullet))
				{
					Main.projectile[bullet].Center = base.Projectile.Center;
				}
				base.Projectile.netUpdate = true;
			}
			SwapSides++;
		}
	}

	private void AttackMovement(int targetIndex)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		Player obj = Main.player[base.Projectile.owner];
		NPC target = Main.npc[targetIndex];
		Vector2 returnPos = Vector2.Zero;
		Vector2 returnPos2 = target.Right + new Vector2(300f, 0f);
		Vector2 returnPos3 = target.Left - new Vector2(300f, 0f);
		returnPos = ((!leftSide) ? returnPos2 : returnPos3);
		if (obj.Center.X - target.Center.X < 0f)
		{
			returnPos = ((!leftSide) ? returnPos3 : returnPos2);
		}
		Vector2 targetVec = returnPos - base.Projectile.Center;
		float num = ((Vector2)(ref targetVec)).Length();
		float targetHomeSpeed = 60f;
		if (num > 100f)
		{
			((Vector2)(ref targetVec)).Normalize();
			targetVec *= targetHomeSpeed;
			base.Projectile.velocity = (base.Projectile.velocity * 10f + targetVec) / 11f;
			base.Projectile.spriteDirection = (base.Projectile.direction = (returnPos.X - base.Projectile.Center.X > 0f).ToDirectionInt());
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? MathHelper.ToRadians(-135f) : MathHelper.ToRadians(-45f));
			ShootCooldown = 40f;
		}
		else
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = (target.Center.X - base.Projectile.Center.X > 0f).ToDirectionInt());
			float angle = base.Projectile.AngleTo(target.Center) + ((base.Projectile.spriteDirection == 1) ? MathHelper.ToRadians(-135f) : MathHelper.ToRadians(-45f));
			base.Projectile.rotation = ((SpinCounter != 0f) ? (angle + SpinCounter) : base.Projectile.rotation.AngleTowards(angle, 0.3f));
			base.Projectile.Center = returnPos + new Vector2(0f, ((float)Math.Sin((float)Math.PI + sineCounter / 50f) * 0.5f + 0.5f) * 40f);
		}
		drawTrail = true;
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
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
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
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? MathHelper.ToRadians(-135f) : MathHelper.ToRadians(-45f));
		}
		else
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = 0);
			base.Projectile.rotation = GetOffsetAngle + (float)Math.PI / 4f;
			drawTrail = false;
			base.Projectile.Center = returnPos;
		}
		SwapSides = 0f;
	}

	public override bool? CanDamage()
	{
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

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	public Color TrailColorFunction(float completionRatio)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		float opacity = (float)Math.Pow(Utils.GetLerpValue(1f, 0.45f, completionRatio, clamped: true), 4.0) * base.Projectile.Opacity * 0.48f;
		return new Color(148, 0, 211) * opacity;
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
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection == 1);
		if (drawTrail)
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
			direction = (SpriteEffects)(direction | 2);
		}
		Main.CurrentDrawnEntityShader = Main.player[base.Projectile.owner]?.cMinion ?? 0;
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction);
		return false;
	}
}
