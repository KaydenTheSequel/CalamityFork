using System.Collections.Generic;
using System.Linq;
using CalamityMod.DataStructures;
using CalamityMod.Enums;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Renderers.CalamityRenderers;

public class DyeableShadersRenderer : BaseRenderer
{
	[Autoload(true, Side = ModSide.Client)]
	private class ItemUpdateHooks : GlobalItem
	{
		public override bool InstancePerEntity => false;

		public override string IsArmorSet(Item head, Item body, Item legs)
		{
			if (head.ModItem is IDyeableShaderRenderer && head.ModItem.IsArmorSet(head, body, legs))
			{
				return "DyeableShaderSet";
			}
			if (body.ModItem is IDyeableShaderRenderer && body.ModItem.IsArmorSet(head, body, legs))
			{
				return "DyeableShaderSet";
			}
			if (legs.ModItem is IDyeableShaderRenderer && legs.ModItem.IsArmorSet(head, body, legs))
			{
				return "DyeableShaderSet";
			}
			return "";
		}

		public override void UpdateItemDye(Item item, Player player, int dye, bool hideVisual)
		{
			if (item.ModItem is IDyeableShaderRenderer drawer)
			{
				drawer.OwnerPlayer = player?.whoAmI ?? 255;
				Dyes[drawer] = dye;
			}
		}

		public override void UpdateVanity(Item item, Player player)
		{
			CheckIfEquipIsValid(item, hideVisual: false);
		}

		public override void UpdateAccessory(Item item, Player player, bool hideVisual)
		{
			CheckIfEquipIsValid(item, hideVisual);
		}

		public override void UpdateArmorSet(Player player, string set)
		{
			Item head = player.armor[0];
			Item body = player.armor[1];
			Item legs = player.armor[2];
			if (head.ModItem != null && head.ModItem.IsArmorSet(head, body, legs) && head.ModItem is IDyeableShaderRenderer renderer)
			{
				MarkAsValid(renderer);
			}
			if (body.ModItem != null && body.ModItem.IsArmorSet(head, body, legs) && body.ModItem is IDyeableShaderRenderer renderer2)
			{
				MarkAsValid(renderer2);
			}
			if (legs.ModItem != null && legs.ModItem.IsArmorSet(head, body, legs) && legs.ModItem is IDyeableShaderRenderer renderer3)
			{
				MarkAsValid(renderer3);
			}
		}
	}

	private static List<IDyeableShaderRenderer> RenderersToDrawThisFrame;

	public static Dictionary<IDyeableShaderRenderer, RenderTargetLease> Targets { get; private set; }

	public static Dictionary<IDyeableShaderRenderer, int> Dyes { get; private set; }

	public override GeneralDrawLayer Layer => GeneralDrawLayer.AfterPlayers;

	public override bool ShouldDraw => true;

	public override void Load()
	{
		Targets = new Dictionary<IDyeableShaderRenderer, RenderTargetLease>();
		Dyes = new Dictionary<IDyeableShaderRenderer, int>();
		RenderersToDrawThisFrame = new List<IDyeableShaderRenderer>();
	}

	public override void Unload()
	{
		Targets?.Clear();
		Targets = null;
		Dyes?.Clear();
		Dyes = null;
		RenderersToDrawThisFrame?.Clear();
		RenderersToDrawThisFrame = null;
	}

	private static void CheckIfEquipIsValid(Item item, bool hideVisual)
	{
		if (!Main.dedServ && (!item.expertOnly || Main.expertMode) && (!item.masterOnly || Main.masterMode) && !(item.IsAir | hideVisual) && item.ModItem != null && item.ModItem is IDyeableShaderRenderer dyeableShaderRenderer)
		{
			MarkAsValid(dyeableShaderRenderer);
		}
	}

	private static void MarkAsValid(IDyeableShaderRenderer renderer)
	{
		if (Main.dedServ)
		{
			return;
		}
		Main.QueueMainThreadAction(delegate
		{
			if (!Targets.ContainsKey(renderer))
			{
				Main.QueueMainThreadAction(delegate
				{
					Targets[renderer] = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice);
				});
			}
		});
		RenderersToDrawThisFrame.AddWithCondition(renderer, renderer.ShouldDrawDyeableShader);
	}

	public override void PreUpdate()
	{
		RenderersToDrawThisFrame?.Clear();
	}

	public override void DrawToTarget(SpriteBatch spriteBatch)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ || RenderersToDrawThisFrame.Count <= 0)
		{
			return;
		}
		RenderersToDrawThisFrame = RenderersToDrawThisFrame.OrderByDescending((IDyeableShaderRenderer dyeableShaderRenderer) => dyeableShaderRenderer.RenderDepth).ToList();
		foreach (IDyeableShaderRenderer renderer in RenderersToDrawThisFrame)
		{
			if (Targets.TryGetValue(renderer, out var target))
			{
				using (target.Scope(preserveContents: true, Color.Transparent))
				{
					renderer.DrawDyeableShader(spriteBatch);
				}
			}
		}
	}

	public override void DrawTarget(SpriteBatch spriteBatch)
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ || RenderersToDrawThisFrame.Count <= 0)
		{
			return;
		}
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
		foreach (IDyeableShaderRenderer renderer in RenderersToDrawThisFrame)
		{
			if (Targets.TryGetValue(renderer, out var target))
			{
				if (renderer.ShaderIsDyeable && Dyes.TryGetValue(renderer, out var dyeID) && dyeID > 0)
				{
					GameShaders.Armor.Apply(dyeID, null, new DrawData((Texture2D)(object)target.Target, Vector2.Zero, (Rectangle?)new Rectangle(0, 0, ((Texture2D)target.Target).Width, ((Texture2D)target.Target).Height), Color.White));
				}
				RenderTarget2D target2 = target.Target;
				Vector2 zero = Vector2.Zero;
				Color white = Color.White;
				((Color)(ref white)).A = 0;
				spriteBatch.Draw((Texture2D)(object)target2, zero, white);
			}
		}
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise);
	}
}
