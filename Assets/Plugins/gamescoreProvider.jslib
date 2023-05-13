mergeInto(LibraryManager.library, {

    /* LANGUAGE */
    GSLanguage: function () {
        var value = GameScore.Language();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSChangeLanguage: function (language) {
        GameScore.ChangeLanguage(UTF8ToString(language));
    },
    /* LANGUAGE */



    /* AVATAR GENERATOR */
    GSChangeAvatarGenerator: function (generator) {
        GameScore.ChangeAvatarGenerator(UTF8ToString(generator));
    },
    GSAvatarGenerator: function () {
        var value = GameScore.AvatarGenerator();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    /* AVATAR GENERATOR */



    /* PLATFORM */
    GSPlatformType: function () {
        var value = GameScore.PlatformType();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },

    GSPlatformHasIntegratedAuth: function () {
        var value = GameScore.PlatformHasIntegratedAuth();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSPlatformIsExternalLinksAllowed: function () {
        var value = GameScore.PlatformIsExternalLinksAllowed();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    /* PLATFORM */



    /* APP */
    GSAppTitle: function () {
        var value = GameScore.AppTitle();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSAppDescription: function () {
        var value = GameScore.AppDescription();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSAppImage: function () {
        var value = GameScore.AppImage();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSAppUrl: function () {
        var value = GameScore.AppUrl();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    /* APP */



    /* PLAYER */
    GSPlayerGetNumberInt: function (key) {
        return GameScore.PlayerGet(UTF8ToString(key));
    },
    GSPlayerGetNumberFloat: function (key) {
        return GameScore.PlayerGet(UTF8ToString(key));
    },
    GSPlayerGetBool: function (key) {
        var value = GameScore.PlayerGet(UTF8ToString(key));
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },

    GSPlayerGetString: function (key) {
        var value = GameScore.PlayerGet(UTF8ToString(key));
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },

    GSPlayerGetID: function () {
        return GameScore.PlayerGetID();
    },
    GSPlayerGetScore: function () {
        return GameScore.PlayerGetScore();
    },
    GSPlayerGetName: function () {
        var value = GameScore.PlayerGetName();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSPlayerGetAvatar: function () {
        var value = GameScore.PlayerGetAvatar();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSPlayerGetFieldName: function (key) {
        return GameScore.PlayerGetFieldName(UTF8ToString(key));
    },
    GSPlayerGetFieldVariantName: function (key, value) {
        return GameScore.PlayerGetFieldVariantName(UTF8ToString(key), UTF8ToString(value));
    },
    GSPlayerGetFieldVariantAt: function (key, index) {
        return GameScore.PlayerGetFieldVariantAt(UTF8ToString(key), UTF8ToString(index));
    },
    GSPlayerGetFieldVariantIndex: function (key, value) {
        return GameScore.PlayerGetFieldVariantIndex(UTF8ToString(key), UTF8ToString(value));
    },

    GSPlayerSetName: function (name) {
        GameScore.PlayerSetName(UTF8ToString(name));
    },
    GSPlayerSetAvatar: function (src) {
        GameScore.PlayerSetAvatar(UTF8ToString(src));
    },
    GSPlayerSetScore: function (score) {
        GameScore.PlayerSetScore(score);
    },
    GSPlayerAddScore: function (score) {
        GameScore.PlayerAddScore(score);
    },
    GSPlayerSet: function (key, value) {
        GameScore.PlayerSet(UTF8ToString(key), UTF8ToString(value));
    },
    GSPlayerSetFlag: function (key, value) {
        GameScore.PlayerSetFlag(UTF8ToString(key), UTF8ToString(value));
    },
    GSPlayerAdd: function (key, value) {
        GameScore.PlayerAdd(UTF8ToString(key), UTF8ToString(value));
    },
    GSPlayerToggle: function (key) {
        GameScore.PlayerToggle(UTF8ToString(key));
    },
    GSPlayerReset: function () {
        GameScore.PlayerReset();
    },
    GSPlayerRemove: function () {
        GameScore.PlayerRemove();
    },
    GSPlayerSync: function (override) {
        GameScore.PlayerSync(override);
    },
    GSPlayerLoad: function () {
        GameScore.PlayerLoad();
    },
    GSPlayerLogin: function () {
        GameScore.PlayerLogin();
    },
    GSPlayerFetchFields: function () {
        GameScore.PlayerFetchFields();
    },

    GSPlayerHas: function (key) {
        var value = GameScore.PlayerHas(UTF8ToString(key));
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },

    GSPlayerIsLoggedIn: function () {
        var value = GameScore.PlayerIsLoggedIn();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSPlayerHasAnyCredentials: function () {
        var value = GameScore.PlayerHasAnyCredentials();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSPlayerIsStub: function () {
        var value = GameScore.PlayerIsStub();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    /* PLAYER */



    /* LEADER BOARD */
    GSLeaderboardOpen: function (orderBy, order, limit, withMe, includeFields, displayFields) {
        GameScore.LeaderboardOpen(UTF8ToString(orderBy), UTF8ToString(order), UTF8ToString(limit), UTF8ToString(withMe), UTF8ToString(includeFields), UTF8ToString(displayFields));
    },
    GSLeaderboardFetch: function (tag, orderBy, order, limit, withMe, includeFields) {
        GameScore.LeaderboardFetch(UTF8ToString(tag), UTF8ToString(orderBy), UTF8ToString(order), UTF8ToString(limit), UTF8ToString(withMe), UTF8ToString(includeFields));
    },
    GSLeaderboardFetchPlayerRating: function (tag, orderBy, order) {
        GameScore.LeaderboardFetchPlayerRating(UTF8ToString(tag), UTF8ToString(orderBy), UTF8ToString(order));
    },
    /* LEADER BOARD */



    /* LEADER BOARD SCOPED */
    GSLeaderboardScopedOpen: function (id, tag, variant, order, limit, includeFields, displayFields, withMe) {
        GameScore.LeaderboardScopedOpen(UTF8ToString(id), UTF8ToString(tag), UTF8ToString(variant), UTF8ToString(order), UTF8ToString(limit), UTF8ToString(includeFields), UTF8ToString(displayFields), UTF8ToString(withMe));
    },
    GSLeaderboardScopedFetch: function (id, tag, variant, order, limit, includeFields, withMe) {
        GameScore.LeaderboardScopedFetch(UTF8ToString(id), UTF8ToString(tag), UTF8ToString(variant), UTF8ToString(order), UTF8ToString(limit), UTF8ToString(includeFields), UTF8ToString(withMe));
    },
    GSLeaderboardScopedPublishRecord: function (id, tag, variant, override, key1, value1, key2, value2, key3, value3) {
        GameScore.LeaderboardScopedPublishRecord(UTF8ToString(id), UTF8ToString(tag), UTF8ToString(variant), override, UTF8ToString(key1), UTF8ToString(value1), UTF8ToString(key2), UTF8ToString(value2), UTF8ToString(key3), UTF8ToString(value3));
    },
    GSLeaderboardScopedFetchPlayerRating: function (id, tag, variant, includeFields) {
        GameScore.LeaderboardScopedFetchPlayerRating(UTF8ToString(id), UTF8ToString(tag), UTF8ToString(variant), UTF8ToString(includeFields));
    },
    /* LEADER BOARD SCOPED */



    /* ACHIEVEMENTS */
    GSAchievementsOpen: function () {
        GameScore.AchievementsOpen();
    },
    GSAchievementsFetch: function () {
        GameScore.AchievementsFetch();
    },
    GSAchievementsUnlock: function (idOrTag) {
        GameScore.AchievementsUnlock(UTF8ToString(idOrTag));
    },
    /* ACHIEVEMENTS */



    /* PAYMENTS */
    GSPaymentsFetchProducts: function () {
        GameScore.PaymentsFetchProducts();
    },
    GSPaymentsPurchase: function (idOrTag) {
        GameScore.PaymentsPurchase(UTF8ToString(idOrTag));
    },
    GSPaymentsConsume: function (idOrTag) {
        GameScore.PaymentsConsume(UTF8ToString(idOrTag));
    },

    GSPaymentsIsAvailable: function () {
        var value = GameScore.PaymentsIsAvailable();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    /* PAYMENTS */



    /* FULLSCREEN */
    GSFullscreenOpen: function () {
        GameScore.FullscreenOpen();
    },
    GSFullscreenClose: function () {
        GameScore.FullscreenClose();
    },
    GSFullscreenToggle: function () {
        GameScore.FullscreenToggle();
    },
    /* FULLSCREEN */



    /* ADS */
    GSAdsShowFullscreen: function () {
        GameScore.AdsShowFullscreen();
    },
    GSAdsShowRewarded: function (Tag) {
        GameScore.AdsShowRewarded(UTF8ToString(Tag));
    },
    GSAdsShowPreloader: function () {
        GameScore.AdsShowPreloader();
    },
    GSAdsShowSticky: function () {
        GameScore.AdsShowSticky();
    },
    GSAdsCloseSticky: function () {
        GameScore.AdsCloseSticky();
    },
    GSAdsRefreshSticky: function () {
        GameScore.AdsRefreshSticky();
    },



    GSAdsIsAdblockEnabled: function () {
        var value = GameScore.AdsIsAdblockEnabled();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },


    GSAdsIsStickyAvailable: function () {
        var value = GameScore.AdsIsStickyAvailable();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;

    },
    GSAdsIsFullscreenAvailable: function () {
        var value = GameScore.AdsIsFullscreenAvailable();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;

    },
    GSAdsIsRewardedAvailable: function () {
        var value = GameScore.AdsIsRewardedAvailable();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSAdsIsPreloaderAvailable: function () {
        var value = GameScore.AdsIsPreloaderAvailable();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSAdsIsStickyPlaying: function () {
        var value = GameScore.AdsIsStickyPlaying();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSAdsIsFullscreenPlaying: function () {
        var value = GameScore.AdsIsFullscreenPlaying();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSAdsIsRewardedPlaying: function () {
        var value = GameScore.AdsIsRewardedPlaying();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSAdsIsPreloaderPlaying: function () {
        var value = GameScore.AdsIsPreloaderPlaying();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    /* ADS */



    /* ANALYTICS */
    GSAnalyticsHit: function (url) {
        GameScore.AnalyticsHit(UTF8ToString(url));
    },
    GSAnalyticsGoal: function (event, value) {
        GameScore.AnalyticsGoal(UTF8ToString(event), UTF8ToString(value));
    },
    /* ANALYTICS */



    /* SOCIALS */
    GSSocialsShare: function (text, url, image) {
        GameScore.SocialsShare(UTF8ToString(text), UTF8ToString(url), UTF8ToString(image));
    },
    GSSocialsPost: function (text, url, image) {
        GameScore.SocialsPost(UTF8ToString(text), UTF8ToString(url), UTF8ToString(image));
    },
    GSSocialsInvite: function (text, url, image) {
        GameScore.SocialsInvite(UTF8ToString(text), UTF8ToString(url), UTF8ToString(image));
    },
    GSSocialsJoinCommunity: function () {
        GameScore.SocialsJoinCommunity();
    },


    GSSocialsCommunityLink: function () {
        var value = GameScore.SocialsCommunityLink();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSSocialsIsSupportsNativeShare: function () {
        var value = GameScore.SocialsIsSupportsNativeShare();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSSocialsIsSupportsNativePosts: function () {
        var value = GameScore.SocialsIsSupportsNativePosts();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSSocialsIsSupportsNativeInvite: function () {
        var value = GameScore.SocialsIsSupportsNativeInvite();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSSocialsCanJoinCommunity: function () {
        var value = GameScore.SocialsCanJoinCommunity();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    GSSocialsIsSupportsNativeCommunityJoin: function () {
        var value = GameScore.SocialsIsSupportsNativeCommunityJoin();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;

    },
    /* SOCIALS */



    /* GAMES COLLECTIONS */
    GSGamesCollectionsOpen: function (idOrTag) {
        GameScore.GamesCollectionsOpen(UTF8ToString(idOrTag));
    },
    GSGamesCollectionsFetch: function (idOrTag) {
        GameScore.GamesCollectionsFetch(UTF8ToString(idOrTag));
    },
    /* GAMES COLLECTIONS */



    /*GAME*/
    GSIsPaused: function () {
        var value = GameScore.IsPaused();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },

    GSPause: function () {
        GameScore.Pause();
    },
    GSResume: function () {
        GameScore.Resume();
    },
    /*GAME*/



    /*DEVICE*/
    GSIsMobile: function () {
        var value = GameScore.IsMobile();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },

    /*DEVICE*/



    /*SERVER*/
    GSServerTime: function () {
        var value = GameScore.ServerTime();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    /*SERVER*/



    /*SYSTEM*/
    GSIsDev: function () {
        var value = GameScore.IsDev();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },

    GSIsAllowedOrigin: function () {
        var value = GameScore.IsAllowedOrigin();
        var bufferSize = lengthBytesUTF8(value) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(value, buffer, bufferSize);
        return buffer;
    },
    /*SYSTEM*/


});
