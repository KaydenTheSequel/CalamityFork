using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SamsaraSlicerProjectile : ModProjectile, ILocalizedModType, IModType
{
	public bool initialized;

	private Vector2 oldVelocity;

	private int? npcTaggedTo;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/SamsaraSlicer";

	public float ReboundVelocity => 30f;

	public float StealthReboundVelocity => 30f;

	public float StealthPauseTime => 55f;

	public int ReboundTime => 30;

	public int SmallDiskDamage => 30;

	public int SmallDiskStealthDamage => 19;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 46);
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.aiStyle = -1;
		base.Projectile.ai[0] = -200f;
		base.Projectile.ai[2] = -200f;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = 20;
		height = 20;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0917: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_092e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_078c: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0801: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Unknown result type (might be due to invalid IL or missing references)
		//IL_0846: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_0864: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, new Vector3(0.1f, 0.5f, 0.03f));
		if (!initialized)
		{
			base.Projectile.ai[2] = -200f;
			base.Projectile.ai[0] = -200f;
			initialized = true;
		}
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.ai[0] < 0f)
		{
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] > (float)ReboundTime)
			{
				base.Projectile.tileCollide = false;
				float lerp = (base.Projectile.ai[1] - (float)ReboundTime) * 0.01f;
				if (base.Projectile.Calamity().stealthStrike)
				{
					lerp = (base.Projectile.ai[1] - (float)ReboundTime) * 0.005f;
				}
				base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.DirectionTo(player.Center) * (base.Projectile.Calamity().stealthStrike ? StealthReboundVelocity : ReboundVelocity), lerp);
				if (base.Projectile.Distance(player.Center) < ((Vector2)(ref base.Projectile.velocity)).Length() * 1.4f)
				{
					for (int i = 0; i < Main.projectile.Length; i++)
					{
						Projectile proj = Main.projectile[i];
						if (proj.type == ModContent.ProjectileType<SamsaraSlicerSmallDisk>() && (proj.ModProjectile as SamsaraSlicerSmallDisk).Parent == base.Projectile)
						{
							(Main.projectile[i].ModProjectile as SamsaraSlicerSmallDisk).Parent = null;
						}
					}
					base.Projectile.Kill();
				}
			}
		}
		else if (npcTaggedTo.HasValue)
		{
			NPC npc = Main.npc[npcTaggedTo ?? 0];
			if (npc.active)
			{
				Projectile projectile = base.Projectile;
				projectile.Center += npc.velocity;
			}
		}
		if (base.Projectile.ai[0] > -150f)
		{
			base.Projectile.ai[0]--;
		}
		if (base.Projectile.ai[0] == 0f)
		{
			npcTaggedTo = null;
			base.Projectile.velocity = oldVelocity;
			SoundEngine.PlaySound(SoundID.DD2_SkyDragonsFuryShot.WithPitchOffset(1f));
			if (base.Projectile.Calamity().stealthStrike)
			{
				SoundEngine.PlaySound(SoundID.Item122.WithPitchOffset(1f), base.Projectile.Center);
				for (int j = 1; j <= 3; j++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, oldVelocity * MathHelper.Lerp(2f, 1f, (float)j / 3f) / 4f, Color.LimeGreen, "CalamityMod/Particles/SoftRoundExplosion", new Vector2(0.6f, 1f), oldVelocity.ToRotation(), 0.02f, 0.05f * (float)j, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
				for (int k = -10; k <= 20; k++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Utils.RotatedBy(new Vector2((float)(k * 2), 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2)), "CalamityMod/Particles/ThinEndedLine", affectedByGravity: false, 10, Main.rand.NextFloat(0.3f, 1f), (Color)(Main.rand.NextBool() ? new Color(1f, 0.8f, 0.1f) : Color.LimeGreen), new Vector2(Main.rand.NextFloat(0.4f, 1f), 1f)));
				}
			}
			else
			{
				for (int l = 1; l <= 2; l++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Utils.RotatedBy(new Vector2((float)((l == 1) ? 2 : 6), 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2)), Color.LimeGreen, "CalamityMod/Particles/BloomRing", new Vector2(0.5f, 1f), base.Projectile.velocity.ToRotation(), 0.1f, 0.5f - (float)l * 0.1f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
			}
			for (int m = 0; m <= 5; m++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Utils.RotatedBy(new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-10f, 10f)), (double)base.Projectile.velocity.ToRotation(), default(Vector2)), "CalamityMod/Particles/ThinEndedLine", affectedByGravity: false, 10, Main.rand.NextFloat(0.3f, 1f), (Color)(Main.rand.NextBool() ? new Color(1f, 0.8f, 0.1f) : Color.LimeGreen), new Vector2(Main.rand.NextFloat(0.4f, 1f), 1f)));
			}
		}
		if (base.Projectile.ai[0] <= 0f && base.Projectile.ai[0] > -4f)
		{
			base.Projectile.extraUpdates = 1;
		}
		else
		{
			base.Projectile.extraUpdates = 0;
		}
		Vector2 vel = base.Projectile.velocity;
		if (base.Projectile.ai[0] > 0f)
		{
			((Vector2)(ref vel))._002Ector(18f);
		}
		if (base.Projectile.ai[2] > -150f)
		{
			base.Projectile.ai[2]--;
		}
		if (base.Projectile.ai[2] > 0f)
		{
			if (base.Projectile.ai[2] % (float)(base.Projectile.Calamity().stealthStrike ? 3 : 5) == 0f)
			{
				SoundEngine.PlaySound(SoundID.Item23.WithPitchOffset(MathHelper.Lerp(1f, 0f, base.Projectile.ai[2] / 30f)).WithVolumeScale(0.8f));
			}
			for (int n = 0; n <= 2; n++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Utils.RotatedBy(new Vector2(25f, 0f), (double)oldVelocity.ToRotation(), default(Vector2)), Utils.RotatedBy(new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-10f, 10f)), (double)oldVelocity.ToRotation(), default(Vector2)), "CalamityMod/Particles/ThinEndedLine", affectedByGravity: false, 10, Main.rand.NextFloat(0.3f, 1f), (Color)(Main.rand.NextBool() ? new Color(1f, 0.8f, 0.1f) : Color.LimeGreen), new Vector2(Main.rand.NextFloat(0.4f, 1f), 1f)));
			}
		}
		if (base.Projectile.ai[2] > -150f && base.Projectile.Calamity().stealthStrike)
		{
			float SpawnVel = 15f;
			float g = Main.rand.NextFloat(360f);
			if (!Main.dedServ)
			{
				(Projectile.NewProjectileDirect(new EntitySource_Parent(base.Projectile), base.Projectile.Center, Utils.RotatedBy(new Vector2(SpawnVel, 0f), (double)MathHelper.ToRadians(g), default(Vector2)), ModContent.ProjectileType<SamsaraSlicerSmallDisk>(), base.Projectile.Calamity().stealthStrike ? SmallDiskStealthDamage : SmallDiskDamage, 1f, base.Projectile.owner, base.Projectile.whoAmI).ModProjectile as SamsaraSlicerSmallDisk).Parent = base.Projectile;
			}
		}
		if (base.Projectile.ai[2] == 0f)
		{
			base.Projectile.localNPCHitCooldown = 30;
		}
		base.Projectile.rotation += MathHelper.ToRadians(((Vector2)(ref vel)).Length() * 1.5f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		float rand = 0f;
		if (base.Projectile.ai[2] > 0f)
		{
			rand = 4f;
		}
		Vector2 randVec = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(0f - rand, rand), 0f), (double)oldVelocity.ToRotation(), default(Vector2));
		Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition + randVec, tex.Frame(), Color.White, base.Projectile.rotation, tex.Frame().Center(), 1f, (SpriteEffects)0);
		if (base.Projectile.ai[2] < 0f)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], new Color(0f, 0.6f, 0f, 0f), 2, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/SamsaraSlicerGlow", (AssetRequestMode)2).Value);
		}
		else
		{
			Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/SamsaraSlicerGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition + randVec, tex.Frame(), new Color(0f, 1f, 0f, 0f), base.Projectile.rotation, tex.Frame().Center(), 1f, (SpriteEffects)0);
		}
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		npcTaggedTo = target.whoAmI;
		if (base.Projectile.velocity != Vector2.Zero)
		{
			if (base.Projectile.ai[0] <= -200f)
			{
				oldVelocity = base.Projectile.velocity * 1.5f;
			}
			else
			{
				oldVelocity = base.Projectile.velocity;
			}
		}
		base.Projectile.ai[1] = ReboundTime - 10;
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.ai[0] = 5f;
		SoundEngine.PlaySound(in SoundID.DD2_WitherBeastCrystalImpact);
		if (base.Projectile.ai[2] != -200f)
		{
			return;
		}
		float lag = 20f;
		if (base.Projectile.Calamity().stealthStrike)
		{
			lag = StealthPauseTime;
		}
		base.Projectile.ai[0] = lag;
		base.Projectile.ai[2] = lag;
		base.Projectile.localNPCHitCooldown = (int)lag;
		float g = Main.rand.NextFloat(360f);
		g -= 15f;
		float SpawnVel = 15f;
		if (base.Projectile.Calamity().stealthStrike)
		{
			SpawnVel = 20f;
		}
		if (!Main.dedServ)
		{
			for (float i = g; i < g + 360f; i += (base.Projectile.Calamity().stealthStrike ? 45f : 90f))
			{
				Projectile projectile = Projectile.NewProjectileDirect(new EntitySource_Parent(base.Projectile), base.Projectile.Center, Utils.RotatedBy(new Vector2(SpawnVel, 0f), (double)MathHelper.ToRadians(i), default(Vector2)), ModContent.ProjectileType<SamsaraSlicerSmallDisk>(), base.Projectile.Calamity().stealthStrike ? SmallDiskStealthDamage : SmallDiskDamage, 1f, base.Projectile.owner, base.Projectile.whoAmI);
				(projectile.ModProjectile as SamsaraSlicerSmallDisk).Parent = base.Projectile;
				projectile.Calamity().stealthStrike = base.Projectile.Calamity().stealthStrike;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.Center);
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		return false;
	}
}
