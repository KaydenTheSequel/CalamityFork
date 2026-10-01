using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class NebulaCloudCore : ModProjectile, ILocalizedModType, IModType
{
	private const float IntendedVelocity = 6f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 3;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0910: Unknown result type (might be due to invalid IL or missing references)
		//IL_0912: Unknown result type (might be due to invalid IL or missing references)
		//IL_0919: Unknown result type (might be due to invalid IL or missing references)
		//IL_0923: Unknown result type (might be due to invalid IL or missing references)
		//IL_092a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_079f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_081c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0825: Unknown result type (might be due to invalid IL or missing references)
		//IL_0827: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		int projectileState = 0;
		if (((Vector2)(ref base.Projectile.velocity)).Length() <= 6f)
		{
			projectileState = 1;
		}
		base.Projectile.alpha -= 15;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		switch (projectileState)
		{
		case 0:
			base.Projectile.rotation -= (float)Math.PI / 30f;
			if (Main.rand.NextBool(3))
			{
				if (Main.rand.NextBool())
				{
					Vector2 prettyDustDirect = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
					Dust obj4 = Main.dust[Dust.NewDust(base.Projectile.Center - prettyDustDirect * 45f, 0, 0, Utils.SelectRandom<int>(Main.rand, 86, 90))];
					obj4.noGravity = true;
					obj4.position = base.Projectile.Center - prettyDustDirect * (float)Main.rand.Next(20, 31);
					obj4.velocity = prettyDustDirect.RotatedBy(1.5707963705062866) * 9f;
					obj4.scale = 0.7f + Main.rand.NextFloat();
					obj4.fadeIn = 0.5f;
					obj4.customData = this;
				}
				else
				{
					Vector2 prettyDustDirect2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
					Dust obj5 = Main.dust[Dust.NewDust(base.Projectile.Center - prettyDustDirect2 * 45f, 0, 0, 240)];
					obj5.noGravity = true;
					obj5.position = base.Projectile.Center - prettyDustDirect2 * 45f;
					obj5.velocity = prettyDustDirect2.RotatedBy(-1.5707963705062866) * 4f;
					obj5.scale = 0.7f + Main.rand.NextFloat();
					obj5.fadeIn = 0.5f;
					obj5.customData = this;
				}
			}
			if (base.Projectile.ai[0] >= 30f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.98f;
				base.Projectile.scale += 0.0074468083f;
				if (base.Projectile.scale > 1.3f)
				{
					base.Projectile.scale = 1.3f;
				}
				base.Projectile.rotation -= (float)Math.PI / 180f;
			}
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 6.1f)
			{
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 6f;
				base.Projectile.ai[0] = 0f;
			}
			break;
		case 1:
		{
			base.Projectile.rotation -= (float)Math.PI / 30f;
			if (Main.rand.NextBool())
			{
				Vector2 slowPrettyDustDirect = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
				Dust obj = Main.dust[Dust.NewDust(base.Projectile.Center - slowPrettyDustDirect * 45f, 0, 0, 86)];
				obj.noGravity = true;
				obj.position = base.Projectile.Center - slowPrettyDustDirect * (float)Main.rand.Next(20, 31);
				obj.velocity = slowPrettyDustDirect.RotatedBy(1.5707963705062866) * 9f;
				obj.scale = 1.2f + Main.rand.NextFloat();
				obj.fadeIn = 0.5f;
				obj.customData = this;
				slowPrettyDustDirect = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
				Dust obj2 = Main.dust[Dust.NewDust(base.Projectile.Center - slowPrettyDustDirect * 45f, 0, 0, 90)];
				obj2.noGravity = true;
				obj2.position = base.Projectile.Center - slowPrettyDustDirect * (float)Main.rand.Next(20, 31);
				obj2.velocity = slowPrettyDustDirect.RotatedBy(1.5707963705062866) * 9f;
				obj2.scale = 1.2f + Main.rand.NextFloat();
				obj2.fadeIn = 0.5f;
				obj2.customData = this;
				obj2.color = Color.Purple;
			}
			else
			{
				Vector2 slowPrettyDustDirect2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
				Dust obj3 = Main.dust[Dust.NewDust(base.Projectile.Center - slowPrettyDustDirect2 * 45f, 0, 0, 240)];
				obj3.noGravity = true;
				obj3.position = base.Projectile.Center - slowPrettyDustDirect2 * (float)Main.rand.Next(30, 41);
				obj3.velocity = slowPrettyDustDirect2.RotatedBy(-1.5707963705062866) * 6f;
				obj3.scale = 1.2f + Main.rand.NextFloat();
				obj3.fadeIn = 0.5f;
				obj3.customData = this;
			}
			if (base.Projectile.ai[0] % 30f == 0f && base.Projectile.ai[0] < 241f && Main.myPlayer == base.Projectile.owner)
			{
				Vector2 randomProjRotate = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * 12f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, randomProjRotate, ModContent.ProjectileType<NebulaCloud>(), base.Projectile.damage / 2, 0f, base.Projectile.owner, 0f, base.Projectile.whoAmI);
			}
			Vector2 projCenter = base.Projectile.Center;
			float homingRange = 1200f;
			bool isHoming = false;
			int npcTrack = 0;
			if (base.Projectile.ai[1] == 0f)
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.CanBeChasedBy(this))
					{
						Vector2 npcCenter = n.Center;
						if (base.Projectile.Distance(npcCenter) < homingRange && Collision.CanHit(new Vector2(base.Projectile.position.X + (float)(base.Projectile.width / 2), base.Projectile.position.Y + (float)(base.Projectile.height / 2)), 1, 1, n.position, n.width, n.height))
						{
							homingRange = base.Projectile.Distance(npcCenter);
							projCenter = npcCenter;
							isHoming = true;
							npcTrack = n.whoAmI;
						}
					}
				}
				if (isHoming)
				{
					if (base.Projectile.ai[1] != (float)(npcTrack + 1))
					{
						base.Projectile.netUpdate = true;
					}
					base.Projectile.ai[1] = npcTrack + 1;
				}
				isHoming = false;
			}
			if (base.Projectile.ai[1] != 0f)
			{
				int npcID = (int)(base.Projectile.ai[1] - 1f);
				if (Main.npc[npcID].active && Main.npc[npcID].CanBeChasedBy(this, ignoreDontTakeDamage: true) && base.Projectile.Distance(Main.npc[npcID].Center) < 1000f)
				{
					isHoming = true;
					projCenter = Main.npc[npcID].Center;
				}
			}
			if (isHoming)
			{
				int inertia = 12;
				Vector2 projCenterHome = base.Projectile.Center;
				float projXDirection = projCenter.X - projCenterHome.X;
				float projYDirection = projCenter.Y - projCenterHome.Y;
				float projDistance = (float)Math.Sqrt(projXDirection * projXDirection + projYDirection * projYDirection);
				projDistance = 6f / projDistance;
				projXDirection *= projDistance;
				projYDirection *= projDistance;
				base.Projectile.velocity.X = (base.Projectile.velocity.X * (float)(inertia - 1) + projXDirection) / (float)inertia;
				base.Projectile.velocity.Y = (base.Projectile.velocity.Y * (float)(inertia - 1) + projYDirection) / (float)inertia;
			}
			break;
		}
		}
		if (base.Projectile.alpha < 150)
		{
			Lighting.AddLight(base.Projectile.Center, 1.4f, 0.4f, 1.2f);
		}
		if (base.Projectile.ai[0] >= 900f)
		{
			base.Projectile.Kill();
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = (0f - oldVelocity.X) * 0.25f;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = (0f - oldVelocity.Y) * 0.25f;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Color fuckYou = base.Projectile.GetAlpha(lightColor);
		Color coreColor = fuckYou * 0.8f;
		((Color)(ref coreColor)).A = (byte)(((Color)(ref coreColor)).A / 2);
		Color cloudColor = Color.Lerp(fuckYou, Color.Black, 0.5f);
		((Color)(ref cloudColor)).A = ((Color)(ref fuckYou)).A;
		float rotationScale = 0.95f + (base.Projectile.rotation * 0.75f).ToRotationVector2().Y * 0.1f;
		cloudColor *= rotationScale;
		float cloudScale = 0.6f + base.Projectile.scale * 0.6f * rotationScale;
		Texture2D coreTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/NebulaCloud", (AssetRequestMode)1).Value;
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		Vector2 coreOrigin = coreTexture.Size() / new Vector2(0f, (float)Main.projFrames[base.Type]) * 0.5f;
		Vector2 cloudOrigin = value.Size() * 0.5f;
		Main.EntitySpriteDraw(value, position, null, cloudColor, 0f - base.Projectile.rotation + 0.35f, cloudOrigin, cloudScale, (SpriteEffects)(spriteEffects ^ 1));
		Main.EntitySpriteDraw(value, position, null, fuckYou, 0f - base.Projectile.rotation, cloudOrigin, base.Projectile.scale, (SpriteEffects)(spriteEffects ^ 1));
		Main.EntitySpriteDraw(coreTexture, position, (Rectangle?)new Rectangle(0, Main.projFrames[base.Type] * base.Projectile.frame, coreTexture.Width, Main.projFrames[base.Type]), coreColor, (0f - base.Projectile.rotation) * 0.7f, coreOrigin, base.Projectile.scale, (SpriteEffects)(spriteEffects ^ 1), 0f);
		Main.EntitySpriteDraw(value, position, null, fuckYou * 0.8f, base.Projectile.rotation * 0.5f, cloudOrigin, base.Projectile.scale * 0.9f, spriteEffects);
		((Color)(ref fuckYou)).A = 0;
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_072a: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_074d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0761: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_077a: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 264);
		base.Projectile.Center = base.Projectile.position;
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		for (int i = 0; i < 6; i++)
		{
			int blackDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 240, 0f, 0f, 100, default(Color), 1.75f);
			Main.dust[blackDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(Math.PI) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
		}
		for (int j = 0; j < 45; j++)
		{
			int purpleDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 62, 0f, 0f, 200, default(Color), 5.05f);
			Main.dust[purpleDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(Math.PI) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			Main.dust[purpleDust].noGravity = true;
			Dust obj = Main.dust[purpleDust];
			obj.velocity *= 4f;
			purpleDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 90, 0f, 0f, 100, default(Color), 1.75f);
			Main.dust[purpleDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(Math.PI) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			Dust obj2 = Main.dust[purpleDust];
			obj2.velocity *= 2.5f;
			Main.dust[purpleDust].noGravity = true;
			Main.dust[purpleDust].fadeIn = 1f;
			Main.dust[purpleDust].color = Color.Purple * 0.5f;
		}
		for (int k = 0; k < 15; k++)
		{
			int purpleDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 62, 0f, 0f, 0, default(Color), 3.55f);
			Main.dust[purpleDust2].position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(Math.PI).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 2f;
			Main.dust[purpleDust2].noGravity = true;
			Dust obj3 = Main.dust[purpleDust2];
			obj3.velocity *= 4f;
		}
		for (int l = 0; l < 15; l++)
		{
			int blackDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 240, 0f, 0f, 0, default(Color), 1.75f);
			Main.dust[blackDust2].position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(Math.PI).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 2f;
			Main.dust[blackDust2].noGravity = true;
			Dust obj4 = Main.dust[blackDust2];
			obj4.velocity *= 4f;
		}
		for (int m = 0; m < 3; m++)
		{
			int gored = Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.position + new Vector2((float)(base.Projectile.width * Main.rand.Next(100)) / 100f, (float)(base.Projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64));
			Main.gore[gored].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(Math.PI) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			Gore obj5 = Main.gore[gored];
			obj5.velocity *= 0.5f;
			Main.gore[gored].velocity.X += (float)Main.rand.Next(-10, 11) * 0.075f;
			Main.gore[gored].velocity.Y += (float)Main.rand.Next(-10, 11) * 0.075f;
		}
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		for (int r = 0; r < Main.maxProjectiles; r++)
		{
			if (Main.projectile[r].active && Main.projectile[r].type == ModContent.ProjectileType<NebulaCloud>() && Main.projectile[r].ai[1] == (float)base.Projectile.whoAmI)
			{
				Main.projectile[r].Kill();
			}
		}
		int totalProjectiles = Main.rand.Next(6, 9);
		float radians = (float)Math.PI * 2f / (float)totalProjectiles;
		int type = ModContent.ProjectileType<NebulaNova>();
		for (int n = 0; n < totalProjectiles; n++)
		{
			Vector2 velocity = -Vector2.UnitY.RotatedBy(radians * (float)n) * Main.rand.NextFloat(7f, 10f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + Main.rand.NextVector2Circular(30f, 30f), velocity, type, (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack * 0.8f, base.Projectile.owner);
		}
	}
}
