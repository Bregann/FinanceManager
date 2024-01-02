import backendFetchHelper from '@/helpers/BackendFetchHelper'
import type { NextApiRequest, NextApiResponse } from 'next'

export interface AddAutomaticTransactionResponse {
  success: boolean
  reason: string
  automaticTransactionId?: number
}

export default async function handler (req: NextApiRequest, res: NextApiResponse<AddAutomaticTransactionResponse>): Promise<void> {
  if (req.method !== 'POST') {
    res.status(405)
  }

  const fetchResult = await backendFetchHelper.doPost('/AutomaticTransactions/AddAutomaticTransaction', req.body)

  if (fetchResult.errored) {
    res.status(500)
  } else {
    res.status(200).json(fetchResult.data as AddAutomaticTransactionResponse)
  }
}
