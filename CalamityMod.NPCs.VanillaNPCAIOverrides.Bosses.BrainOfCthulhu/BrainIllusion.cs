using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;

[AutoloadBossHead]
public class BrainIllusion : ModNPC, ILocalizedModType, IModType
{
	private Vector2 OldPos;

	public new string LocalizationCategory => "NPCs";

	public override string Texture => "Terraria/Images/NPC_266";

	public override string BossHeadTexture => "Terraria/Images/NPC_Head_Boss_23";

	private ref float Time => ref base.NPC.ai[0];

	private ref float AttackValue => ref base.NPC.ai[1];

	private ref float Angle => ref base.NPC.ai[2];

	private ref float TeleportDuration => ref base.NPC.localAI[0];

	private ref float TeleportTime => ref base.NPC.ai[3];

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 160;
		base.NPC.height = 110;
		base.NPC.damage = 20;
		base.NPC.lifeMax = 1;
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.boss = true;
		base.NPC.dontTakeDamage = true;
		base.NPC.chaseable = false;
		base.NPC.npcSlots = 0f;
		base.NPC.netAlways = true;
		base.Music = 13;
		base.SceneEffectPriority = (SceneEffectPriority)(-1);
	}

	public override void OnSpawn(IEntitySource source)
	{
		TeleportDuration = AttackValue;
		AttackValue = 0f;
		TeleportTime = Time;
		base.NPC.netUpdate = true;
	}

	public override void AI()
	{
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		if (NPC.crimsonBoss == -1)
		{
			base.NPC.active = false;
			return;
		}
		NPC brain = Main.npc[NPC.crimsonBoss];
		if (brain.AIOverride<BrainOfCthulhuAI>().AIState == BrainOfCthulhuAI.BrainAIState.DeathAnimation)
		{
			base.NPC.damage = 0;
			base.NPC.Opacity -= 0.1f;
			if (base.NPC.Opacity <= 0f)
			{
				base.NPC.active = false;
			}
		}
		Player target = Main.player[base.NPC.target];
		base.NPC.GivenName = brain.GivenOrTypeName + $": {brain.life}/{brain.lifeMax}";
		if (Time < 30f)
		{
			TeleportDuration = 30f;
			if (Time == TeleportDuration / 2f && AttackValue == 0f)
			{
				AttackValue = 1f;
			}
			else
			{
				TeleportTime--;
			}
			base.NPC.Opacity = 1f - TeleportTime / (TeleportDuration / 2f);
		}
		else
		{
			if (Time == 30f)
			{
				if (Main.netMode != 1)
				{
					OldPos = base.NPC.Center;
					TeleportTime = 0f;
					AttackValue = 0f;
					base.NPC.netUpdate = true;
				}
				base.NPC.Opacity = 1f;
			}
			if (Time < 60f)
			{
				float lerp = (Time - 30f) / 30f;
				float circleDist = MathHelper.Lerp(BrainOfCthulhuAI.IllusionDashTeleportDistance, BrainOfCthulhuAI.IllusionDashCloseInDistance, CalamityUtils.SineInOutEasing(lerp, 1));
				base.NPC.Center = Vector2.Lerp(OldPos, target.Center + Angle.ToRotationVector2() * circleDist, lerp);
				Angle += MathHelper.Lerp(0f, BrainOfCthulhuAI.IllusionDashStartingSpinSpeed, CalamityUtils.SineInEasing(lerp, 1));
			}
			else if (Time <= 60f + (float)BrainOfCthulhuAI.IllusionDashSpinDuration)
			{
				base.NPC.Center = target.Center + Angle.ToRotationVector2() * BrainOfCthulhuAI.IllusionDashCloseInDistance;
				Angle += MathHelper.Lerp(BrainOfCthulhuAI.IllusionDashStartingSpinSpeed, 0f, CalamityUtils.SineOutEasing((Time - 60f) / (float)BrainOfCthulhuAI.IllusionDashSpinDuration, 1));
			}
			else if (Time <= (float)(90 + BrainOfCthulhuAI.IllusionDashSpinDuration))
			{
				float reelBackSpeedExponent = 2.6f;
				float reelBackCompletion = Utils.GetLerpValue(0f, 30f, Time - (float)(60 + BrainOfCthulhuAI.IllusionDashSpinDuration), clamped: true);
				float reelBackSpeed = MathHelper.Lerp(4f, 20f, MathF.Pow(reelBackCompletion, reelBackSpeedExponent));
				Vector2 reelBackVelocity = (Angle + (float)Math.PI).ToRotationVector2() * (0f - reelBackSpeed);
				base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, reelBackVelocity, 0.25f);
			}
			else if (Time == (float)(91 + BrainOfCthulhuAI.IllusionDashSpinDuration))
			{
				base.NPC.velocity = (Angle + (float)Math.PI).ToRotationVector2() * BrainOfCthulhuAI.IllusionDashVelocity;
			}
			else if (Time <= (float)(91 + BrainOfCthulhuAI.IllusionDashSpinDuration + BrainOfCthulhuAI.IllusionDashTeleportDuration))
			{
				base.NPC.Opacity = MathHelper.Lerp(1f, 0.25f, CalamityUtils.SineInEasing((Time - (float)(91 + BrainOfCthulhuAI.IllusionDashSpinDuration)) / (float)BrainOfCthulhuAI.IllusionDashTeleportDuration, 1));
				if (base.NPC.Opacity < 0.666f)
				{
					base.NPC.damage = 0;
				}
			}
			else
			{
				base.NPC.active = false;
			}
		}
		Time++;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		writer.WritePackedWorldPosition(OldPos);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		OldPos = reader.ReadPackedWorldPosition();
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 6.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
		}
		if (base.NPC.frame.Y < frameHeight * 4)
		{
			base.NPC.frame.Y = frameHeight * 4;
		}
		if (base.NPC.frame.Y > frameHeight * 7)
		{
			base.NPC.frame.Y = frameHeight * 4;
		}
	}

	public override bool? CanBeHitByItem(Player player, Item item)
	{
		return false;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		return false;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		return target.whoAmI == base.NPC.target;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPos = base.NPC.Center + Vector2.UnitY * 16f - Main.screenPosition;
		Vector2 scale = Vector2.One;
		float opacityMult = ((Main.LocalPlayer.whoAmI == base.NPC.target) ? 1f : 0.25f);
		if (TeleportTime != 0f)
		{
			scale = Vector2.Lerp(Vector2.One, new Vector2(0.5f + ((float)Math.Cos(Time / (TeleportDuration / 2f) * ((float)Math.PI * 2f)) / 2f + 0.5f), 0.5f + ((float)Math.Sin(Time / (TeleportDuration / 2f) * ((float)Math.PI * 2f)) / 2f + 0.5f)), CalamityUtils.SineInOutEasing(TeleportTime / (TeleportDuration / 2f), 1));
			spriteBatch.Draw(TextureAssets.Npc[266].Value, drawPos, (Rectangle?)base.NPC.frame, Lighting.GetColor(base.NPC.Center.ToTileCoordinates()) * base.NPC.Opacity * opacityMult, base.NPC.rotation, base.NPC.frame.Size() * 0.5f, scale * base.NPC.scale, (SpriteEffects)0, 0f);
		}
		else
		{
			spriteBatch.Draw(TextureAssets.Npc[266].Value, drawPos, (Rectangle?)base.NPC.frame, Lighting.GetColor(base.NPC.Center.ToTileCoordinates()) * base.NPC.Opacity * opacityMult, base.NPC.rotation, base.NPC.frame.Size() * 0.5f, scale * base.NPC.scale, (SpriteEffects)0, 0f);
		}
		return false;
	}

	public BrainIllusion()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		OldPos = Vector2.zeroVector;
		base._002Ector();
	}
}
