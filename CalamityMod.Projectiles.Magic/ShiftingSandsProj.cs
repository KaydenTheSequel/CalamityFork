using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ShiftingSandsProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 3;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 12;
	}

	public override void AI()
	{
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		int maxVelocity = 32;
		Player player = Main.player[base.Projectile.owner];
		int height = Main.maxTilesY * 16;
		int heightRatio = 0;
		if (base.Projectile.ai[0] >= 0f)
		{
			heightRatio = (int)(base.Projectile.ai[1] / (float)height);
		}
		bool notBeingChanneled = base.Projectile.ai[0] == -1f || base.Projectile.ai[0] == -2f;
		if (base.Projectile.penetrate == 1 && base.Projectile.ai[0] >= 0f && heightRatio == 0)
		{
			base.Projectile.ai[1] += height;
			heightRatio = 1;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.penetrate == 1 && base.Projectile.ai[0] == -1f)
		{
			base.Projectile.ai[0] = -2f;
			base.Projectile.netUpdate = true;
		}
		if (heightRatio > 0 || base.Projectile.ai[0] == -2f)
		{
			base.Projectile.localAI[0]++;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (base.Projectile.ai[0] >= 0f)
			{
				if (player.channel && player.HeldItem.shoot == base.Projectile.type)
				{
					Vector2 channelPos = Main.MouseWorld;
					player.LimitPointToPlayerReachableArea(ref channelPos);
					if (base.Projectile.ai[0] != channelPos.X || base.Projectile.ai[1] != channelPos.Y)
					{
						base.Projectile.netUpdate = true;
						base.Projectile.ai[0] = channelPos.X;
						base.Projectile.ai[1] = channelPos.Y + (float)(height * heightRatio);
					}
				}
				else
				{
					base.Projectile.netUpdate = true;
					base.Projectile.ai[0] = -1f;
					base.Projectile.ai[1] = -1f;
					NPC homeTarget = ClosestNPCAtMagicMissileStyle(base.Projectile.Center);
					if (homeTarget != null)
					{
						int targetIndex = homeTarget.whoAmI;
						if (targetIndex != -1)
						{
							base.Projectile.ai[1] = targetIndex;
						}
					}
					else if (((Vector2)(ref base.Projectile.velocity)).Length() < 2f)
					{
						base.Projectile.velocity = base.Projectile.DirectionFrom(player.Center) * (float)maxVelocity;
					}
					else
					{
						base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.Zero) * (float)maxVelocity;
					}
				}
			}
			if (notBeingChanneled && base.Projectile.ai[1] == -1f)
			{
				NPC homeTarget2 = ClosestNPCAtMagicMissileStyle(base.Projectile.Center);
				if (homeTarget2 != null)
				{
					int targetIndex2 = homeTarget2.whoAmI;
					if (targetIndex2 != -1)
					{
						base.Projectile.ai[1] = targetIndex2;
						base.Projectile.netUpdate = true;
					}
				}
			}
		}
		Vector2 targetVector = Vector2.Zero;
		float chaseLerp = 1f;
		if (base.Projectile.ai[0] > 0f && base.Projectile.ai[1] > 0f)
		{
			((Vector2)(ref targetVector))._002Ector(base.Projectile.ai[0], base.Projectile.ai[1] % (float)height);
		}
		if (notBeingChanneled && base.Projectile.ai[1] >= 0f)
		{
			base.Projectile.tileCollide = false;
			NPC target = Main.npc[(int)base.Projectile.ai[1]];
			if (target.CanBeChasedBy())
			{
				targetVector = target.Center;
				float invFineL = Utils.GetLerpValue(0f, 100f, base.Projectile.Distance(targetVector), clamped: true) * Utils.GetLerpValue(600f, 400f, base.Projectile.Distance(targetVector), clamped: true);
				chaseLerp = MathHelper.Lerp(0f, 0.2f, Utils.GetLerpValue(200f, 20f, 1f - invFineL, clamped: true));
			}
			else
			{
				base.Projectile.ai[1] = -1f;
				base.Projectile.netUpdate = true;
			}
		}
		if (targetVector != Vector2.Zero)
		{
			if (base.Projectile.Distance(targetVector) >= 64f)
			{
				Vector2 distanceVector = targetVector - base.Projectile.Center;
				Vector2 moveVelocity = distanceVector.SafeNormalize(Vector2.Zero);
				float velocityMult = Math.Min(maxVelocity, ((Vector2)(ref distanceVector)).Length());
				moveVelocity *= velocityMult;
				if (((Vector2)(ref base.Projectile.velocity)).Length() < 4f)
				{
					Projectile projectile = base.Projectile;
					projectile.velocity += base.Projectile.velocity.RotatedBy(0.7853981852531433).SafeNormalize(Vector2.Zero) * 4f;
				}
				base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, moveVelocity, chaseLerp);
			}
			else
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.3f;
				Projectile projectile3 = base.Projectile;
				projectile3.velocity += (targetVector - base.Projectile.Center) * 0.3f;
			}
			if (base.Projectile.timeLeft < 60)
			{
				base.Projectile.timeLeft = 60;
			}
		}
		if (notBeingChanneled && base.Projectile.ai[1] < 0f)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() != (float)maxVelocity)
			{
				base.Projectile.velocity = base.Projectile.velocity.MoveTowards(base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * (float)maxVelocity, 4f);
			}
			if (base.Projectile.timeLeft > 300)
			{
				base.Projectile.timeLeft = 300;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	private NPC ClosestNPCAtMagicMissileStyle(Vector2 origin, float maxDistanceToCheck = 800f)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		NPC closestTarget = null;
		float distance = maxDistanceToCheck;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (Main.npc[i].CanBeChasedBy() && base.Projectile.localNPCImmunity[i] == 0)
			{
				_ = Main.npc[i].width / 2;
				_ = Main.npc[i].height / 2;
				if (Vector2.Distance(origin, Main.npc[i].Center) < distance)
				{
					distance = Vector2.Distance(origin, Main.npc[i].Center);
					closestTarget = Main.npc[i];
				}
			}
		}
		return closestTarget;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return base.Projectile.ai[0] < 0f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.ai[0] == -1f)
		{
			base.Projectile.ai[1] = -1f;
			base.Projectile.netUpdate = true;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 64);
		base.Projectile.Center = base.Projectile.position;
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		int dustAmt = 36;
		for (int i = 0; i < dustAmt; i++)
		{
			Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
			Vector2 dustVel = val - base.Projectile.Center;
			int sand = Dust.NewDust(val + dustVel, 0, 0, 85, dustVel.X * 1.5f, dustVel.Y * 1.5f, 100, default(Color), 1.2f);
			Main.dust[sand].noGravity = true;
			Main.dust[sand].noLight = true;
			Main.dust[sand].velocity = dustVel;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
