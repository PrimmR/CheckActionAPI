using StardewModdingAPI;
using StardewValley;
using Microsoft.Xna.Framework;

namespace CheckActionAPI
{
    /// <summary>A class containing the static <c>check</c> method.</summary>
    public sealed class CheckAction
    {
        /// <summary>An approximation of the checks done before an item is used.</summary>
        /// <param name="button">An <c>SButton</c>> from a key press event.</param>
        /// <returns>A boolean repsesenting whether the action button should trigger an item usage</returns>
        public static bool Check(SButton button)
        {
            // Ignore if player isn't in control
            if (!Context.CanPlayerMove || !Context.IsWorldReady)
                return false;

            // Check that action button actually pressed
            if (!button.IsActionButton())
                return false;

            // Check player state
            bool allowed = (
                !Game1.player.hasMenuOpen.Value &&
                Game1.player.canMove &&
                !Game1.player.isRidingHorse() &&
                !(Game1.eventUp && !Game1.isFestival()) &&
                !Game1.dialogueUp &&
                !Game1.dialogueTyping &&
                !Game1.player.UsingTool &&
                (!Game1.eventUp || (Game1.currentLocation.currentEvent != null && Game1.currentLocation.currentEvent.playerControlSequence)) &&
                !Game1.eventUp &&
                !Game1.isFestival() &&
                !Game1.fadeToBlack &&
                !Game1.player.swimming.Value &&
                !Game1.player.bathingClothes.Value &&
                !Game1.player.onBridge.Value
            );
            if (!allowed)
                return false;

            // Check if player is interacting with tile instead
            Vector2 grabTile = new Vector2(Game1.getOldMouseX() + Game1.viewport.X, Game1.getOldMouseY() + Game1.viewport.Y) / 64f;
            bool was_character_at_grab_tile = Game1.player.currentLocation.isCharacterAtTile(grabTile) != null;
            if (!Game1.wasMouseVisibleThisFrame || Game1.mouseCursorTransparency == 0f || !Utility.tileWithinRadiusOfPlayer((int)grabTile.X, (int)grabTile.Y, 1, Game1.player))
                grabTile = Game1.player.GetGrabTile();
            if (Game1.tryToCheckAt(grabTile, Game1.player))
                return false;
            grabTile.Y += 1f;
            if (Game1.player.FacingDirection >= 0 && Game1.player.FacingDirection <= 3)
            {
                Vector2 normalized_offset = grabTile - Game1.player.Tile;
                if (normalized_offset.X > 0f || normalized_offset.Y > 0f)
                {
                    normalized_offset.Normalize();
                }
                if (Vector2.Dot(Utility.DirectionsTileVectors[Game1.player.FacingDirection], normalized_offset) >= 0f && Game1.tryToCheckAt(grabTile, Game1.player))
                {
                    return false;
                }
            }
            grabTile.Y -= 2f;
            if (Game1.player.FacingDirection >= 0 && Game1.player.FacingDirection <= 3 && !was_character_at_grab_tile)
            {
                Vector2 normalized_offset = grabTile - Game1.player.Tile;
                if (normalized_offset.X > 0f || normalized_offset.Y > 0f)
                {
                    normalized_offset.Normalize();
                }
                if (Vector2.Dot(Utility.DirectionsTileVectors[Game1.player.FacingDirection], normalized_offset) >= 0f && Game1.tryToCheckAt(grabTile, Game1.player))
                {
                    return false;
                }
                grabTile = Game1.player.Tile;
                if (Game1.tryToCheckAt(grabTile, Game1.player))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
