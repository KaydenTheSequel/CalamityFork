using System;
using System.IO;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PlantationStaffTentacle : ModProjectile, ILocalizedModType, IModType
{
	public enum AIState
	{
		Attached,
		Seeking
	}

	public Vector2 DesiredLocation;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer ModdedOwner => Owner.Calamity();

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center.MinionHoming(PlantationStaff.EnemyDistanceDetection, Owner);
		}
	}

	public Projectile MainMinion => Main.projectile[(int)MainMinionIndex];

	public ref float TentacleIndex => ref base.Projectile.ai[0];

	public ref float MainMinionIndex => ref base.Projectile.ai[1];

	public ref float AITimer => ref base.Projectile.localAI[0];

	public AIState State
	{
		get
		{
			return (AIState)base.Projectile.ai[2];
		}
		set
		{
			base.Projectile.ai[2] = (float)value;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.width = (base.Projectile.height = 22);
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.netImportant = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(AITimer);
		writer.WritePackedVector2(DesiredLocation);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		AITimer = reader.ReadSingle();
		DesiredLocation = reader.ReadPackedVector2();
	}

	public override void AI()
	{
		CheckMinionExistence();
		DoAnimation();
		switch (State)
		{
		case AIState.Attached:
			AttachedState();
			break;
		case AIState.Seeking:
			SeekingState();
			break;
		}
	}

	private void AttachedState()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		AITimer++;
		float interpolant = Utils.Remap(AITimer, 0f, PlantationStaff.TimeBeforeRamming, 0f, 0.4f);
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, MainMinion.Center + DesiredLocation, interpolant);
		base.Projectile.rotation = (base.Projectile.Center - MainMinion.Center).ToRotation();
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile proj = enumerator.Current;
			if (proj.owner == Owner.whoAmI && proj.type == ModContent.ProjectileType<PlantationStaffSummon>() && proj.ModProjectile<PlantationStaffSummon>().State != PlantationStaffSummon.AIState.Ramming)
			{
				State = AIState.Seeking;
				AITimer = 0f;
				base.Projectile.velocity = Vector2.Zero;
				base.Projectile.penetrate = 1;
				for (int dustIndex = 0; dustIndex < 20; dustIndex++)
				{
					Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 40);
				}
				SoundEngine.PlaySound(in SoundID.NPCDeath1, base.Projectile.Center);
				base.Projectile.netUpdate = true;
			}
		}
	}

	private void SeekingState()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (Target != null)
		{
			AITimer++;
			if (AITimer <= 30f)
			{
				base.Projectile.velocity = base.Projectile.rotation.ToRotationVector2() * 10f;
				return;
			}
			base.Projectile.velocity = (base.Projectile.velocity * 35f + base.Projectile.SafeDirectionTo(Target.Center) * PlantationStaff.TentacleSpeed) / 36f;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		else
		{
			base.Projectile.Kill();
		}
	}

	private void CheckMinionExistence()
	{
		if (base.Projectile.ai[1] < 0f || base.Projectile.ai[1] >= (float)Main.maxProjectiles)
		{
			base.Projectile.Kill();
		}
		else if (base.Projectile.type != ModContent.ProjectileType<PlantationStaffTentacle>() || !MainMinion.active || MainMinion.type != ModContent.ProjectileType<PlantationStaffSummon>())
		{
			base.Projectile.Kill();
		}
		else if (ModdedOwner.PlantationSummon)
		{
			base.Projectile.timeLeft = 2;
		}
	}

	private void DoAnimation()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 3)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		DesiredLocation = ((float)Math.PI / 3f * TentacleIndex).ToRotationVector2().RotatedByRandom(0.5235987901687622) * 100f;
		base.Projectile.netUpdate = true;
	}

	public override bool? CanDamage()
	{
		if (State != AIState.Seeking)
		{
			return false;
		}
		return null;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 10; i++)
		{
			Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 40);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		if (MainMinionIndex < 0f || MainMinionIndex >= (float)Main.maxProjectiles)
		{
			return false;
		}
		if (base.Type != ModContent.ProjectileType<PlantationStaffTentacle>() || !MainMinion.active || MainMinion.type != ModContent.ProjectileType<PlantationStaffSummon>())
		{
			return false;
		}
		if (State == AIState.Attached)
		{
			Vector2 source = MainMinion.Center;
			Texture2D chain = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/PlantationStaffTentacleChain", (AssetRequestMode)2).Value;
			Vector2 goal = base.Projectile.Center;
			Rectangle? sourceRectangle = null;
			float textureHeight = chain.Height;
			Vector2 drawVector = source - goal;
			float rotation = drawVector.ToRotation() - (float)Math.PI / 2f;
			bool shouldDraw = true;
			if (float.IsNaN(goal.X) && float.IsNaN(goal.Y))
			{
				shouldDraw = false;
			}
			if (float.IsNaN(drawVector.X) && float.IsNaN(drawVector.Y))
			{
				shouldDraw = false;
			}
			while (shouldDraw)
			{
				if (((Vector2)(ref drawVector)).Length() < textureHeight + 1f)
				{
					shouldDraw = false;
					continue;
				}
				Vector2 value2 = drawVector;
				((Vector2)(ref value2)).Normalize();
				goal += value2 * textureHeight;
				drawVector = source - goal;
				Color color = Lighting.GetColor((int)goal.X / 16, (int)(goal.Y / 16f));
				Main.EntitySpriteDraw(chain, goal - Main.screenPosition, sourceRectangle, color, rotation, chain.Size() / 2f, 1f, (SpriteEffects)0);
			}
		}
		else if (CalamityClientConfig.Instance.Afterimages)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		}
		return true;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		if (!(TentacleIndex < 5f) && !(MainMinionIndex < 0f) && !(MainMinionIndex >= (float)Main.maxProjectiles) && base.Projectile.type == ModContent.ProjectileType<PlantationStaffTentacle>() && MainMinion.active && MainMinion.type == ModContent.ProjectileType<PlantationStaffSummon>())
		{
			Texture2D texture = TextureAssets.Projectile[MainMinion.type].Value;
			int height = texture.Height / Main.projFrames[MainMinion.type];
			int frameHeight = height * MainMinion.frame;
			SpriteEffects spriteEffects = (SpriteEffects)0;
			if (MainMinion.spriteDirection == -1)
			{
				spriteEffects = (SpriteEffects)1;
			}
			Color color = Lighting.GetColor((int)MainMinion.Center.X / 16, (int)(MainMinion.Center.Y / 16f));
			Main.EntitySpriteDraw(texture, MainMinion.Center - Main.screenPosition + new Vector2(0f, MainMinion.gfxOffY), (Rectangle?)new Rectangle(0, frameHeight, texture.Width, height), color, MainMinion.rotation, new Vector2((float)texture.Width / 2f, (float)height / 2f), MainMinion.scale, spriteEffects, 0f);
		}
	}
}
