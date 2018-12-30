using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using AutoPay.Infrastructure.Services;
using AutoPay.Models.Request.Payment;
using AutoPay.Models.Response.Payment;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace AutoPay.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfigurationSection _authorizeNetConfiguraton;

        public PaymentService(IConfiguration configuration)
        {
            _authorizeNetConfiguraton = configuration.GetSection("AuthorizeNet");
        }

        public TransactionResponseModel MakePayment(TransactionRequestModel model)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment
                = _authorizeNetConfiguraton["Environment"].Equals("Sandbox", StringComparison.InvariantCultureIgnoreCase)
                ? AuthorizeNet.Environment.SANDBOX
                : AuthorizeNet.Environment.PRODUCTION;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType
            {
                name = _authorizeNetConfiguraton["LoginId"],
                ItemElementName = ItemChoiceType.transactionKey,
                Item = _authorizeNetConfiguraton["TransactionKey"]
            };
            var creditCard = new creditCardType
            {
                cardNumber = model.CardNumber,
                expirationDate = model.CardExpiration,
                cardCode = model.Ccv
            };

            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),   // charge the card
                amount = model.Amount,
                payment = paymentType
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var transactionResponse = new TransactionResponseModel();
            try
            {
                var controller = new createTransactionController(request);

                controller.Execute();

                var response = controller.GetApiResponse();

                if (controller.GetResultCode() == messageTypeEnum.Ok)
                {
                    transactionResponse.IsSuccess = response.messages.resultCode == messageTypeEnum.Ok;
                    transactionResponse.AuthCode = response.transactionResponse.authCode;
                    transactionResponse.TransactionId = response.transactionResponse.transId;
                    transactionResponse.Errors =
                        response.transactionResponse.errors?
                            .Select(x => new TransactionErrorModel
                            {
                                Code = x.errorCode,
                                Description = x.errorText
                            });
                }
                else
                {
                    var errorResponse = controller.GetErrorResponse();

                    transactionResponse.Errors = errorResponse.messages.message.Select(x => new TransactionErrorModel
                    {
                        Code = x.code,
                        Description = x.text
                    });
                }
            }
            catch (Exception ex)
            {
                transactionResponse.Exception = ex;
            }

            return transactionResponse;
        }
    }
}
