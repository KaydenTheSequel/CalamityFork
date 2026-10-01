using System;
using System.IO;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.PrimordialWyrm;

[LongDistanceNetSync(SyncWith = typeof(PrimordialWyrmHead))]
public class PrimordialWyrmBodyAlt : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public static int FireballDamage = 110;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.PrimordialWyrmHead.DisplayName");

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		this.HideFromBestiary();
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "_Lightmask", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.width = 110;
		base.NPC.height = 88;
		base.NPC.defense = 0;
		base.NPC.LifeMaxNERB(2500000, 3000000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.Opacity = 0f;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath6;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
		base.NPC.dontTakeDamage = true;
		base.NPC.chaseable = false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.localAI[0] = reader.ReadSingle();
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		bool death = CalamityWorld.death;
		bool revenge = CalamityWorld.revenge;
		bool expertMode = Main.expertMode;
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		bool shouldDespawn = !NPC.AnyNPCs(ModContent.NPCType<PrimordialWyrmHead>());
		if (!shouldDespawn)
		{
			if (base.NPC.ai[1] <= 0f)
			{
				shouldDespawn = true;
			}
			else if (Main.npc[(int)base.NPC.ai[1]].life <= 0)
			{
				shouldDespawn = true;
			}
		}
		if (shouldDespawn)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			base.NPC.active = false;
		}
		CalamityGlobalNPC calamityGlobalNPC_Head = Main.npc[(int)base.NPC.ai[2]].Calamity();
		float chargePhaseGateValue = (death ? 180f : (revenge ? 210f : (expertMode ? 240f : 300f)));
		float lightningChargePhaseGateValue = (death ? 120f : (revenge ? 135f : (expertMode ? 150f : 180f)));
		bool num = calamityGlobalNPC_Head.newAI[2] >= chargePhaseGateValue && calamityGlobalNPC_Head.newAI[2] <= chargePhaseGateValue + 1f && (calamityGlobalNPC_Head.newAI[0] == 0f || calamityGlobalNPC_Head.newAI[0] == 4f || calamityGlobalNPC_Head.newAI[0] == 2f);
		bool invisiblePartOfLightningChargePhase = calamityGlobalNPC_Head.newAI[2] >= lightningChargePhaseGateValue && calamityGlobalNPC_Head.newAI[2] <= lightningChargePhaseGateValue + 1f && calamityGlobalNPC_Head.newAI[0] == 8f;
		bool invisiblePhase = calamityGlobalNPC_Head.newAI[0] == 1f || calamityGlobalNPC_Head.newAI[0] == 5f || calamityGlobalNPC_Head.newAI[0] == 7f;
		if (!num && !invisiblePartOfLightningChargePhase && !invisiblePhase)
		{
			if (Main.npc[(int)base.NPC.ai[1]].Opacity > 0.5f)
			{
				base.NPC.Opacity += 0.2f;
				if (base.NPC.Opacity > 1f)
				{
					base.NPC.Opacity = 1f;
				}
			}
		}
		else
		{
			base.NPC.Opacity -= 0.05f;
			if (base.NPC.Opacity < 0f)
			{
				base.NPC.Opacity = 0f;
			}
		}
		if (((calamityGlobalNPC_Head.newAI[0] == 6f && calamityGlobalNPC_Head.newAI[2] > 0f) || (calamityGlobalNPC_Head.newAI[0] == 10f && calamityGlobalNPC_Head.newAI[1] > 0f)) && Vector2.Distance(base.NPC.Center, Main.player[Main.npc[(int)base.NPC.ai[2]].target].Center) > 160f)
		{
			base.NPC.localAI[0]++;
			float shootShadowFireballGateValue = (death ? 70f : (revenge ? 75f : (expertMode ? 80f : 90f)));
			float divisor = 2f;
			if (base.NPC.ai[3] % divisor == 0f && base.NPC.localAI[0] >= shootShadowFireballGateValue)
			{
				base.NPC.localAI[0] = 0f;
				if (Main.netMode != 1)
				{
					float distanceVelocityBoost = MathHelper.Clamp((Vector2.Distance(Main.npc[(int)base.NPC.ai[2]].Center, Main.player[Main.npc[(int)base.NPC.ai[2]].target].Center) - 1600f) * 0.025f, 0f, 16f);
					float fireballVelocity = (Main.player[Main.npc[(int)base.NPC.ai[2]].target].Calamity().ZoneAbyssLayer4 ? 6f : 8f) + distanceVelocityBoost;
					Vector2 velocity = Vector2.Normalize(Main.player[Main.npc[(int)base.NPC.ai[2]].target].Center - base.NPC.Center) * fireballVelocity;
					int type = 468;
					int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity, type, FireballDamage, 0f, Main.myPlayer);
					Main.projectile[proj].tileCollide = false;
				}
			}
		}
		NPC aheadSegment = Main.npc[(int)base.NPC.ai[1]];
		Vector2 directionToNextSegment = aheadSegment.Center - base.NPC.Center;
		if (aheadSegment.rotation != base.NPC.rotation)
		{
			directionToNextSegment = directionToNextSegment.RotatedBy(MathHelper.WrapAngle(aheadSegment.rotation - base.NPC.rotation) * 0.08f);
			directionToNextSegment = directionToNextSegment.MoveTowards((aheadSegment.rotation - base.NPC.rotation).ToRotationVector2(), 1f);
		}
		base.NPC.rotation = directionToNextSegment.ToRotation() + (float)Math.PI / 2f;
		base.NPC.Center = aheadSegment.Center - directionToNextSegment.SafeNormalize(Vector2.Zero) * base.NPC.scale * (float)base.NPC.width;
		base.NPC.spriteDirection = (directionToNextSegment.X > 0f).ToDirectionInt();
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 vector = default(Vector2);
		((Vector2)(ref vector))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		Vector2 center = base.NPC.Center - screenPos;
		center -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		center += vector * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture, center, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, vector, base.NPC.scale, spriteEffects, 0f);
		float brightness = 1f;
		float num = (float)Main.GameUpdateCount * 0.01f;
		float saneVelocity = MathHelper.Clamp((float)(int)PrimordialWyrmHead.PWHeadVelocity, 6f, 8f);
		brightness = MathF.Sin(num * (6f + saneVelocity) - (float)base.NPC.whoAmI);
		brightness = MathHelper.Clamp(brightness, 0.25f, 1f);
		texture = GlowTexture.Value;
		spriteBatch.Draw(texture, center, (Rectangle?)base.NPC.frame, Color.White * (base.NPC.Opacity * brightness), base.NPC.rotation, vector, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0)
		{
			for (int k = 0; k < 10; k++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("PrimordialWyrm4").Type);
			}
		}
	}

	public override void ModifyTypeName(ref string typeName)
	{
		if (Main.zenithWorld)
		{
			typeName = CalamityUtils.GetTextValue("NPCs.Jared");
		}
	}
}
