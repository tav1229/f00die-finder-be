namespace f00die_finder_be.Common
{
    public static class MailConsts
    {
        public static class ForgotPassword
        {
            public const string Subject = "[f00diefinder.id.vn] Đặt lại mật khẩu";
            public const string Body = "ForgotPassword.html";
        }

        public static class VerifyEmail
        {
            public const string Subject = "[f00diefinder.id.vn] Xác nhận địa chỉ email";
            public const string Body = "VerifyEmail.html";
        }

        public static class NewReservationNotification
        {
            public const string Subject = "[f00diefinder.id.vn] Thông báo đơn đặt bàn mới";
            public const string Body = "NewReservationNotification.html";
        }

        public static class ReservationConfirmedNotification
        {
            public const string Subject = "[f00diefinder.id.vn] Thông báo đơn đặt bàn đã được duyệt";
            public const string Body = "ReservationConfirmedNotification.html";
        }

        public static class ReservationDeniedNotification
        {
            public const string Subject = "[f00diefinder.id.vn] Thông báo đơn đặt bàn đã bị từ chối";
            public const string Body = "ReservationDeniedNotification.html";
        }
    }
}
