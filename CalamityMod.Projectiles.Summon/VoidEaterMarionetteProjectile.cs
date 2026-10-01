using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.NPCs;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class VoidEaterMarionetteProjectile : BaseWormProjectile, ILocalizedModType, IModType
{
	public enum AttackState
	{
		Idle,
		DivinityDevourer,
		FaithIncinerator,
		UltracosmicMaelstrom,
		PlayingFetch
	}

	public Vector2 EntrancePortalLocation;

	public Vector2 ExitPortalLocation;

	private bool TightHoming;

	public bool FocusOnFetching;

	public float JawOpeningAmount;

	private List<Asset<Texture2D>> internalTexAssetsGlow;

	private Asset<Texture2D> GlowTexAsset;

	private Asset<Texture2D> Jaws;

	private Asset<Texture2D> JawGlow;

	private Asset<Texture2D> DoGJaws;

	public float ContactDamageMult => 0.6f + base.Projectile.minionSlots * 0.4f;

	public static int EffectiveContactIframes => 10;

	public float BlueFireballDamageMult => base.Projectile.minionSlots * 1.1f;

	public static int EffectiveBlueFireIframes => 10;

	public float PurpleFireballDamageMult => base.Projectile.minionSlots * 1.1f;

	public static int EffectivePurpleFireIframes => 10;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Summon/VoidEaterMarionetteHead";

	public override string GlowTexture => "CalamityMod/Projectiles/Summon/VoidEaterMarionetteHeadGlow";

	public override List<string> SegmentTextures => new List<string> { "CalamityMod/Projectiles/Summon/VoidEaterMarionetteBody", "CalamityMod/Projectiles/Summon/VoidEaterMarionetteTail" };

	public override int SegmentCount => (int)base.Projectile.minionSlots;

	public override List<float> SegmentTypePositionOffsets => new List<float> { 54f, 38f, 52f };

	public int MinionSlotsToAdd
	{
		get
		{
			return (int)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public AttackState CurrentAttack
	{
		get
		{
			return (AttackState)base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = (float)value;
		}
	}

	public int AiTimer
	{
		get
		{
			return (int)base.Projectile.ai[2];
		}
		set
		{
			base.Projectile.ai[2] = value;
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer ModOwner => Owner.Calamity();

	public List<Asset<Texture2D>> SegmentTextureAssetsGlow
	{
		get
		{
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			if (internalTexAssetsGlow.Count == 0)
			{
				for (int i = 0; i < SegmentTextures.Count; i++)
				{
					internalTexAssetsGlow.Add(ModContent.Request<Texture2D>(SegmentTextures[i] + "Glow", (AssetRequestMode)2));
					if (SegmentTypeDrawOffsets.Count <= i)
					{
						SegmentTypeDrawOffsets.Add(Vector2.Zero);
					}
				}
			}
			return internalTexAssetsGlow;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.netImportant = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 5;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.localNPCHitCooldown = EffectiveContactIframes * base.Projectile.MaxUpdates;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		base.SendExtraAI(writer);
		writer.Write(FocusOnFetching);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.ReceiveExtraAI(reader);
		FocusOnFetching = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1020: Unknown result type (might be due to invalid IL or missing references)
		//IL_102b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1030: Unknown result type (might be due to invalid IL or missing references)
		//IL_1035: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0784: Unknown result type (might be due to invalid IL or missing references)
		//IL_0789: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee4: Unknown result type (might be due to invalid IL or missing references)
		//IL_079f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1004: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0838: Unknown result type (might be due to invalid IL or missing references)
		//IL_0855: Unknown result type (might be due to invalid IL or missing references)
		//IL_085b: Unknown result type (might be due to invalid IL or missing references)
		//IL_085d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0867: Unknown result type (might be due to invalid IL or missing references)
		//IL_086c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0880: Unknown result type (might be due to invalid IL or missing references)
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2c: Unknown result type (might be due to invalid IL or missing references)
		Owner.AddBuff(ModContent.BuffType<VoidEaterMarionetteBuff>(), 3600);
		if (Owner.dead)
		{
			ModOwner.hasVoidEaterMarionette = false;
		}
		if (ModOwner.hasVoidEaterMarionette)
		{
			base.Projectile.timeLeft = 2;
		}
		if (MinionSlotsToAdd > 0)
		{
			float minionSlotsAvaliable = Owner.maxMinions;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile item = enumerator.Current;
				if (item.owner == base.Projectile.owner)
				{
					minionSlotsAvaliable -= item.minionSlots;
				}
			}
			while (minionSlotsAvaliable >= 1f && MinionSlotsToAdd > 0)
			{
				base.Projectile.minionSlots++;
				minionSlotsAvaliable--;
				MinionSlotsToAdd--;
			}
			MinionSlotsToAdd = 0;
		}
		bool UpdatedSegmentCount = false;
		while (Segments.Count < SegmentCount)
		{
			UpdatedSegmentCount = true;
			Segments.Add(new BaseWormSegment(this));
		}
		while (Segments.Count > SegmentCount)
		{
			UpdatedSegmentCount = true;
			Segments.RemoveAt(Segments.Count - 1);
		}
		if (UpdatedSegmentCount)
		{
			foreach (BaseWormSegment segment in Segments)
			{
				segment.segmentType = 0;
			}
			Segments[Segments.Count - 1].segmentType = 1;
		}
		if (base.Projectile.minionSlots < 1f)
		{
			base.Projectile.Kill();
			return;
		}
		int targetID = -1;
		base.Projectile.Minion_FindTargetInRange(300.TilesToPixels(), ref targetID, skipIfCannotHitWithOwnBody: false);
		NPC target = base.Projectile.Center.MinionHoming((CurrentAttack != AttackState.Idle) ? 999999f : 2800f, Owner);
		SegmentRigidity = 0.1f;
		base.Projectile.extraUpdates = 1;
		if (Owner.miscCounter % 10 == 0 && (CurrentAttack == AttackState.Idle || (FocusOnFetching && CurrentAttack != AttackState.UltracosmicMaelstrom)))
		{
			float SearchDistance = 50.TilesToPixels();
			AiTimer = -1;
			ActiveEntityIterator<Item>.Enumerator enumerator3 = Main.ActiveItems.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				Item item2 = enumerator3.Current;
				if (item2.noGrabDelay == 0 && !item2.beingGrabbed && Owner.CanPullItem(item2, Owner.ItemSpace(item2)) && !item2.IsACoin)
				{
					float dis = item2.Distance(base.Projectile.Center);
					if (!(dis > SearchDistance))
					{
						AiTimer = item2.whoAmI;
						SearchDistance = dis;
					}
				}
			}
			if (AiTimer > -1)
			{
				CurrentAttack = AttackState.PlayingFetch;
			}
			else if (CurrentAttack == AttackState.PlayingFetch)
			{
				CurrentAttack = AttackState.Idle;
			}
		}
		switch (CurrentAttack)
		{
		case AttackState.Idle:
		{
			float playerDistance = base.Projectile.Distance(Owner.Center);
			float speed = 0.06f;
			AiTimer = -1;
			if (playerDistance > (float)150.TilesToPixels())
			{
				base.Projectile.Center = Owner.Center - base.Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 200f;
				SpawnRiftProjectileAt(base.Projectile.Center);
				base.Projectile.velocity = base.Projectile.velocity.ClampMagnitude(1f, 8f);
			}
			else
			{
				if (playerDistance > 1000f)
				{
					speed = 0.3f;
				}
				if (playerDistance > 200f)
				{
					speed = 0.2f;
				}
				else if (playerDistance > 140f)
				{
					speed = 0.12f;
				}
				if (playerDistance > 100f)
				{
					Projectile projectile6 = base.Projectile;
					projectile6.velocity += base.Projectile.DirectionTo(Owner.Center) * speed;
				}
				else if (((Vector2)(ref base.Projectile.velocity)).Length() > 1f)
				{
					Projectile projectile7 = base.Projectile;
					projectile7.velocity *= 0.96f;
				}
				base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			}
			if (target != null)
			{
				CurrentAttack = AttackState.DivinityDevourer;
				AiTimer = 0;
			}
			JawOpeningAmount = MathHelper.Lerp(JawOpeningAmount, 0f, 0.1f);
			break;
		}
		case AttackState.DivinityDevourer:
		{
			if (target == null)
			{
				CurrentAttack = AttackState.Idle;
				AiTimer = 0;
				TightHoming = false;
				break;
			}
			float targetDistance = base.Projectile.Distance(target.Center);
			float turnspeed = 0.01f;
			if (targetDistance > 500f)
			{
				TightHoming = true;
			}
			if (TightHoming)
			{
				JawOpeningAmount = MathHelper.Lerp(JawOpeningAmount, 0.75f, 0.1f);
				turnspeed = 0.2f;
				if (Vector2.Dot(base.Projectile.velocity.SafeNormalize(Vector2.Zero), base.Projectile.DirectionTo(target.Center)) < 0f && targetDistance < 200f)
				{
					TightHoming = false;
					turnspeed = 0f;
				}
			}
			else
			{
				JawOpeningAmount = MathHelper.Lerp(JawOpeningAmount, 0f, 0.25f);
			}
			if (targetDistance > 0f)
			{
				base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleLerp(base.Projectile.DirectionTo(target.Center).ToRotation(), turnspeed).ToRotationVector2() * MathF.Min(((Vector2)(ref base.Projectile.velocity)).Length() + 1f, 22f);
			}
			Projectile projectile5 = base.Projectile;
			projectile5.velocity *= 0.975f;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			AiTimer++;
			if (AiTimer > 300 * base.Projectile.MaxUpdates)
			{
				CurrentAttack = AttackState.FaithIncinerator;
				AiTimer = 0;
				TightHoming = false;
			}
			break;
		}
		case AttackState.FaithIncinerator:
		{
			if (target == null)
			{
				CurrentAttack = AttackState.Idle;
				AiTimer = 0;
				break;
			}
			float num = base.Projectile.Distance(target.Center);
			Vector2 targetDir = base.Projectile.DirectionTo(target.Center);
			Owner.DirectionTo(target.Center);
			base.Projectile.Distance(Owner.Center);
			Vector2 targetedPosition = target.Center;
			float turnspeed2 = 0.05f;
			if (TightHoming)
			{
				targetedPosition -= targetDir * 960f;
			}
			else
			{
				turnspeed2 = 0.15f;
			}
			base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleLerp(base.Projectile.DirectionTo(targetedPosition).ToRotation(), turnspeed2).ToRotationVector2() * MathF.Min(((Vector2)(ref base.Projectile.velocity)).Length() + 1f, 10f);
			if (num > 800f)
			{
				TightHoming = false;
			}
			else
			{
				Rectangle hitbox = target.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(base.Projectile.Hitbox))
				{
					TightHoming = true;
				}
			}
			JawOpeningAmount = MathHelper.Lerp(JawOpeningAmount, 0f, 0.1f);
			if (Vector2.Dot(base.Projectile.velocity.SafeNormalize(Vector2.Zero), targetDir) > 0.8f && AiTimer % 30 == 0)
			{
				JawOpeningAmount = 0.75f;
				if (Main.myPlayer == base.Projectile.owner)
				{
					for (int j = 0; j < 3; j++)
					{
						Vector2 perturbedSpeed2 = targetDir.RotatedBy(MathHelper.Lerp(-0.15f, 0.15f, (float)j / 3f)) * 24f;
						int p2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, perturbedSpeed2, ModContent.ProjectileType<DoGFire>(), (int)((float)base.Projectile.damage * BlueFireballDamageMult), base.Projectile.knockBack, base.Projectile.owner, 2f);
						if (Main.projectile.IndexInRange(p2))
						{
							Main.projectile[p2].hostile = false;
							Main.projectile[p2].friendly = true;
							Main.projectile[p2].DamageType = DamageClass.Summon;
							Main.projectile[p2].timeLeft = 120;
							Main.projectile[p2].scale = 0.5f;
							Main.projectile[p2].usesIDStaticNPCImmunity = true;
							Main.projectile[p2].idStaticNPCHitCooldown = EffectiveBlueFireIframes;
						}
					}
				}
			}
			AiTimer++;
			if (AiTimer > 300 * base.Projectile.MaxUpdates)
			{
				CurrentAttack = AttackState.UltracosmicMaelstrom;
				AiTimer = 0;
				TightHoming = false;
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			break;
		}
		case AttackState.UltracosmicMaelstrom:
		{
			int strikes = 6;
			Vector2 targetPos = Owner.Center;
			if (target != null)
			{
				targetPos = target.Center;
			}
			if (AiTimer > 0)
			{
				JawOpeningAmount = 0.5f;
			}
			float lastSegmentOpacity = Segments[Segments.Count - 1].Opacity;
			if (AiTimer == 0 && lastSegmentOpacity > 0f && EntrancePortalLocation == Vector2.Zero)
			{
				base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 16f;
				EntrancePortalLocation = base.Projectile.Center + base.Projectile.velocity * 10f;
			}
			if (base.Projectile.Distance(EntrancePortalLocation) <= 32f && AiTimer <= strikes && base.Projectile.Opacity > 0f)
			{
				base.Projectile.Opacity = 0f;
				SpawnRiftProjectileAt(EntrancePortalLocation);
			}
			foreach (BaseWormSegment item4 in Segments)
			{
				if (item4.Center.Distance(EntrancePortalLocation) <= 32f)
				{
					if (AiTimer <= strikes)
					{
						item4.Opacity = 0f;
					}
					else if (item4.segmentType > 0)
					{
						CurrentAttack = AttackState.DivinityDevourer;
						AiTimer = 0;
						EntrancePortalLocation = Vector2.Zero;
						ExitPortalLocation = Vector2.Zero;
						break;
					}
				}
				else if (item4.Center.Distance(ExitPortalLocation) <= 32f)
				{
					item4.Opacity = 1f;
				}
			}
			if (lastSegmentOpacity == 0f && base.Projectile.Opacity == 0f)
			{
				Vector2 dir = Utils.RotatedByRandom(new Vector2(300f), 6.2831854820251465);
				EntrancePortalLocation = targetPos + dir;
				ExitPortalLocation = targetPos - dir;
				base.Projectile.Center = ExitPortalLocation;
				base.Projectile.velocity = dir.SafeNormalize(Vector2.Zero) * 16f;
				base.Projectile.Opacity = 1f;
				foreach (BaseWormSegment segment2 in Segments)
				{
					segment2.Center = base.Projectile.Center - dir * 10f;
				}
				Segments[Segments.Count - 1].Opacity = 0.001f;
				AiTimer++;
				SpawnRiftProjectileAt(ExitPortalLocation);
				if (Main.myPlayer == base.Projectile.owner)
				{
					for (int i = 0; i < 16; i++)
					{
						Vector2 perturbedSpeed = Vector2.UnitX.RotatedBy((float)Math.PI * 2f * (float)i / 16f) * ((i % 2 == 0) ? 24f : 16f);
						int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, perturbedSpeed, ModContent.ProjectileType<DoGFire>(), (int)((float)base.Projectile.damage * PurpleFireballDamageMult), base.Projectile.knockBack, base.Projectile.owner);
						if (Main.projectile.IndexInRange(p))
						{
							Main.projectile[p].hostile = false;
							Main.projectile[p].friendly = true;
							Main.projectile[p].DamageType = DamageClass.Summon;
							Main.projectile[p].timeLeft = 120;
							Main.projectile[p].scale = 0.5f;
							Main.projectile[p].usesIDStaticNPCImmunity = true;
							Main.projectile[p].idStaticNPCHitCooldown = EffectivePurpleFireIframes;
						}
					}
				}
			}
			if (AiTimer > 0)
			{
				base.Projectile.extraUpdates = 2;
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			break;
		}
		case AttackState.PlayingFetch:
		{
			Item item3 = Main.item[AiTimer];
			base.Projectile.Center.DirectionTo(item3.Center);
			Vector2 holdingSpot = base.Projectile.Center + (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * 35f;
			if (!item3.active || item3.beingGrabbed || !Owner.CanPullItem(item3, Owner.ItemSpace(item3)))
			{
				AiTimer = 0;
				CurrentAttack = AttackState.Idle;
				break;
			}
			float dis2 = MathHelper.Min(item3.Distance(holdingSpot), item3.Distance(base.Projectile.Center));
			if (dis2 < 36f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity += base.Projectile.DirectionTo(Owner.Center) * 0.5f;
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.95f;
				item3.Center = holdingSpot;
				item3.velocity = Vector2.Zero;
				JawOpeningAmount = MathHelper.Lerp(JawOpeningAmount, 0f, 0.25f);
			}
			else
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity += base.Projectile.DirectionTo(item3.Center) * 0.5f;
				Projectile projectile4 = base.Projectile;
				projectile4.velocity *= 0.95f;
				JawOpeningAmount = MathF.Max(0f, MathHelper.Lerp(1f, 0f, dis2 / 240f));
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			break;
		}
		}
		Projectile projectile8 = base.Projectile;
		projectile8.position += base.Projectile.velocity;
		base.Projectile.position.X = MathHelper.Clamp(base.Projectile.position.X, 5f, (float)((Main.maxTilesX - 5) * 16));
		base.Projectile.position.Y = MathHelper.Clamp(base.Projectile.position.Y, 5f, (float)((Main.maxTilesY - 5) * 16));
		Projectile projectile9 = base.Projectile;
		projectile9.position -= base.Projectile.velocity;
		UpdateSegments();
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= ContactDamageMult;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Segments.Count; i++)
		{
			BaseWormSegment prevSeg = new BaseWormSegment(this);
			if (i != 0)
			{
				prevSeg = Segments[i - 1];
			}
			if (!((double)prevSeg.Opacity <= 0.001) && !((double)Segments[i].Opacity <= 0.001))
			{
				float cpoint = 0f;
				if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), prevSeg.Center, Segments[i].Center, 16f, ref cpoint))
				{
					return true;
				}
			}
		}
		return base.Colliding(projHitbox, targetHitbox);
	}

	public void SpawnRiftProjectileAt(Vector2 position)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), position, Vector2.Zero, ModContent.ProjectileType<DoGWeaponTeleportRift>(), 0, 0f, base.Projectile.owner, 0f, 0.375f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		for (int i = Segments.Count - 1; i >= 0; i--)
		{
			DrawSegment(ref lightColor, Segments[i]);
		}
		Texture2D jawTex = CalamityUtils.GetTextureEfficient(ref Jaws, "CalamityMod/Projectiles/Summon/VoidEaterMarionetteJaw").Value;
		Texture2D jawGlowTex = CalamityUtils.GetTextureEfficient(ref JawGlow, "CalamityMod/Projectiles/Summon/VoidEaterMarionetteJawGlow").Value;
		Main.spriteBatch.Draw(TextureAssets.Projectile[base.Type].Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, lightColor * base.Projectile.Opacity, base.Projectile.rotation, TextureAssets.Projectile[base.Type].Value.Size() / 2f, base.Projectile.scale, (SpriteEffects)0, 1f);
		Vector2 jawOffset = default(Vector2);
		((Vector2)(ref jawOffset))._002Ector(10f, -18f);
		Main.spriteBatch.Draw(jawTex, base.Projectile.Center - jawOffset.RotatedBy(base.Projectile.rotation) - Main.screenPosition, (Rectangle?)null, lightColor * base.Projectile.Opacity, base.Projectile.rotation - JawOpeningAmount, jawTex.Size() / 2f - jawOffset, base.Projectile.scale, (SpriteEffects)0, 1f);
		if (CurrentAttack != AttackState.DivinityDevourer)
		{
			Main.spriteBatch.Draw(jawGlowTex, base.Projectile.Center - jawOffset.RotatedBy(base.Projectile.rotation) - Main.screenPosition, (Rectangle?)null, Color.White * base.Projectile.Opacity, base.Projectile.rotation - JawOpeningAmount, jawTex.Size() / 2f - jawOffset, base.Projectile.scale, (SpriteEffects)0, 1f);
		}
		jawOffset.X *= -1f;
		Main.spriteBatch.Draw(jawTex, base.Projectile.Center - jawOffset.RotatedBy(base.Projectile.rotation) - Main.screenPosition, (Rectangle?)null, lightColor * base.Projectile.Opacity, base.Projectile.rotation + JawOpeningAmount, jawTex.Size() / 2f - jawOffset, base.Projectile.scale, (SpriteEffects)1, 1f);
		if (CurrentAttack != AttackState.DivinityDevourer)
		{
			Main.spriteBatch.Draw(jawGlowTex, base.Projectile.Center - jawOffset.RotatedBy(base.Projectile.rotation) - Main.screenPosition, (Rectangle?)null, Color.White * base.Projectile.Opacity, base.Projectile.rotation + JawOpeningAmount, jawTex.Size() / 2f - jawOffset, base.Projectile.scale, (SpriteEffects)1, 1f);
		}
		return false;
	}

	private void DrawSegmentGlow(BaseWormSegment segment)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		Lighting.GetColor(segment.Center.ToTileCoordinates());
		if (SegmentTextureAssetsGlow.IndexInRange(segment.segmentType) && (segment.segmentType != 0 || CurrentAttack != AttackState.FaithIncinerator) && (segment.segmentType != 1 || CurrentAttack != AttackState.DivinityDevourer))
		{
			Texture2D tex = SegmentTextureAssetsGlow[segment.segmentType].Value;
			Main.spriteBatch.Draw(tex, segment.Center - Main.screenPosition, (Rectangle?)null, Color.White * segment.Opacity, segment.rotation, tex.Size() / 2f + SegmentTypeDrawOffsets[segment.segmentType], base.Projectile.scale, (SpriteEffects)0, 1f);
		}
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		for (int i = Segments.Count - 1; i >= 0; i--)
		{
			DrawSegmentGlow(Segments[i]);
		}
		if (CurrentAttack != AttackState.FaithIncinerator)
		{
			Main.EntitySpriteDraw(CalamityUtils.GetTextureEfficient(ref GlowTexAsset, GlowTexture).Value, base.Projectile.Center - Main.screenPosition, null, Color.White * base.Projectile.Opacity, base.Projectile.rotation, CalamityUtils.GetTextureEfficient(ref GlowTexAsset, GlowTexture).Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		if (base.Projectile.Opacity > 0f && CurrentAttack == AttackState.UltracosmicMaelstrom && AiTimer > 0)
		{
			Texture2D tex = CalamityUtils.GetTextureEfficient(ref DoGJaws, "CalamityMod/Particles/Jaws").Value;
			Main.spriteBatch.SetBlendState(BlendState.Additive);
			Main.spriteBatch.Draw(tex, base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 12f - Main.screenPosition, (Rectangle?)null, Color.Fuchsia, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, tex.Size() * 0.5f, 0.7f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(tex, base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 12f - Main.screenPosition, (Rectangle?)null, Color.Aqua, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, tex.Size() * 0.5f, 0.6f, (SpriteEffects)0, 0f);
			Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		}
	}

	public VoidEaterMarionetteProjectile()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		EntrancePortalLocation = Vector2.Zero;
		ExitPortalLocation = Vector2.Zero;
		internalTexAssetsGlow = new List<Asset<Texture2D>>();
		base._002Ector();
	}
}
