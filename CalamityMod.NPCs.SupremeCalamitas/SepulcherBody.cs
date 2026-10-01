using System;
using System.IO;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SupremeCalamitas;

[LongDistanceNetSync(SyncWith = typeof(SepulcherHead))]
public class SepulcherBody : ModNPC
{
	private bool setAlpha;

	public static Asset<Texture2D> AltTexture;

	public NPC AheadSegment => Main.npc[(int)base.NPC.ai[1]];

	public NPC HeadSegment => Main.npc[(int)base.NPC.ai[2]];

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.SepulcherHead.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		if (!Main.dedServ)
		{
			AltTexture = ModContent.Request<Texture2D>(Texture + "Alt", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.npcSlots = 5f;
		base.NPC.width = (base.NPC.height = 48);
		base.NPC.defense = 0;
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		calamityGlobalNPC.DR = 0.999999f;
		calamityGlobalNPC.unbreakableDR = true;
		base.NPC.lifeMax = (CalamityWorld.revenge ? 345000 : 300000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.scale *= (Main.expertMode ? 1.35f : 1.2f);
		base.NPC.alpha = 255;
		base.NPC.chaseable = false;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.canGhostHeal = false;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.localAI[3]);
		writer.Write(setAlpha);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.localAI[3] = reader.ReadSingle();
		setAlpha = reader.ReadBoolean();
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		bool shouldDie = false;
		if (base.NPC.ai[1] <= 0f)
		{
			shouldDie = true;
		}
		else if (AheadSegment.life <= 0 || !AheadSegment.active || base.NPC.life <= 0)
		{
			shouldDie = true;
		}
		if (shouldDie)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
		}
		if (AheadSegment.alpha < 128 && !setAlpha)
		{
			if (base.NPC.alpha != 0)
			{
				for (int i = 0; i < 2; i++)
				{
					Dust dust = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 182, 0f, 0f, 100, default(Color), 2f);
					dust.noGravity = true;
					dust.noLight = true;
				}
			}
			base.NPC.alpha -= 42;
			if (base.NPC.alpha <= 0)
			{
				setAlpha = true;
				base.NPC.alpha = 0;
			}
		}
		else
		{
			base.NPC.alpha = HeadSegment.alpha;
		}
		if (Main.npc.IndexInRange((int)base.NPC.ai[1]))
		{
			Vector2 offsetToAheadSegment = AheadSegment.Center - base.NPC.Center;
			base.NPC.rotation = offsetToAheadSegment.ToRotation() + (float)Math.PI / 2f;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.Center = AheadSegment.Center - offsetToAheadSegment.SafeNormalize(Vector2.UnitY) * 52f;
			base.NPC.spriteDirection = (offsetToAheadSegment.X > 0f).ToDirectionInt();
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
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
		Texture2D texture2D15 = ((base.NPC.localAI[3] / 2f % 2f == 0f) ? AltTexture.Value : TextureAssets.Npc[base.Type].Value);
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0 || Main.dedServ)
		{
			return;
		}
		if ((int)(base.NPC.localAI[3] / 2f % 2f) == 0)
		{
			for (int i = 1; i <= 9; i++)
			{
				if (Main.rand.NextBool(3))
				{
					Vector2 goreSpawnPosition = base.NPC.Center;
					Gore.NewGorePerfect(base.NPC.GetSource_Death(), goreSpawnPosition, Main.rand.NextVector2Circular(2f, 2f), base.Mod.Find<ModGore>($"SepulcherBody1_Gore{i}").Type, base.NPC.scale);
				}
			}
			return;
		}
		for (int j = 1; j <= 7; j++)
		{
			if (Main.rand.NextBool(3))
			{
				Vector2 goreSpawnPosition2 = base.NPC.Center;
				Gore.NewGorePerfect(base.NPC.GetSource_Death(), goreSpawnPosition2, Main.rand.NextVector2Circular(2f, 2f), base.Mod.Find<ModGore>($"SepulcherBody2_Gore{j}").Type, base.NPC.scale);
			}
		}
	}
}
