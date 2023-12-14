import { notifications } from '@mantine/notifications'
import { type ReactNode } from 'react'

class notificationHelper {
  public static showSuccessNotification = (notificationTitle: string, notificationMessage: string, i: ReactNode): void => {
    notifications.show({
      withBorder: true,
      title: notificationTitle,
      message: notificationMessage,
      color: 'green',
      icon: i
    })
  }
}

export default notificationHelper
